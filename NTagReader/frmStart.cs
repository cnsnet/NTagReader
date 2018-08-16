using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.Runtime.InteropServices;
using NdefLibrary.Ndef;

namespace NTagReader
{
    public partial class frmStart : Form
    {
        public frmStart()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //uint a = CardReader.GetDllVersion();
        }

        public int retCode, hContext, hCard, Protocol;
        public bool connActive = false;
        public byte[] SendBuff = new byte[16];
        public byte[] RecvBuff = new byte[16];
        public byte[] SendBuffAll = new byte[16];
        public int SendLen, RecvLen;
        public int reqType, Aprotocol;
        public ModWinsCard.SCARD_IO_REQUEST pioSendRequest;

        private void btnInit_Click(object sender, EventArgs e)
        {
            int pcchReaders = 0;

            retCode = ModWinsCard.SCardEstablishContext(ModWinsCard.SCARD_SCOPE_USER, 0, 0, ref hContext);
            if (retCode != ModWinsCard.SCARD_S_SUCCESS)
            {
                WriteLog(1, retCode, "");

                return;
            }

            retCode = ModWinsCard.SCardListReaders(this.hContext, null, null, ref pcchReaders);
            if (retCode != ModWinsCard.SCARD_S_SUCCESS)
            {
                WriteLog(1, retCode, "");

                return;
            }

            byte[] ReadersList = new byte[pcchReaders];

            //Fill reader list
            retCode = ModWinsCard.SCardListReaders(this.hContext, null, ReadersList, ref pcchReaders);
            if (retCode != ModWinsCard.SCARD_S_SUCCESS)
            {
                txtMessage.AppendText("SCardListReaders Error: " + ModWinsCard.GetScardErrMsg(retCode));

                return;
            }
            else
            {
                WriteLog(0, 0, " ");
            }

            //Convert reader buffer to string
            int idx = 0;
            string rName = "";
            while (ReadersList[idx] != 0)
            {
                while (ReadersList[idx] != 0)
                {
                    rName = rName + (char)ReadersList[idx];
                    idx = idx + 1;
                }
                //Add reader name to list
                ddlReaderList.Items.Add(rName);
                rName = "";
                idx = idx + 1;
            }

            if (ddlReaderList.Items.Count > 0)
            {
                ddlReaderList.SelectedIndex = 0;
            }
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            retCode = ModWinsCard.SCardConnect(hContext, ddlReaderList.SelectedItem.ToString(), ModWinsCard.SCARD_SHARE_SHARED,
                                           ModWinsCard.SCARD_PROTOCOL_T1, ref hCard, ref Protocol);

            if (retCode != ModWinsCard.SCARD_S_SUCCESS)
            {
                retCode = ModWinsCard.SCardConnect(hContext, ddlReaderList.SelectedItem.ToString(), ModWinsCard.SCARD_SHARE_DIRECT,
                                            0, ref hCard, ref Protocol);
                if (retCode != ModWinsCard.SCARD_S_SUCCESS)
                    WriteLog(1, retCode, "");
                else
                {
                    WriteLog(0, 0, "Connected to " + ddlReaderList.Text);//Successful connection to
                }
            }
            else
            {
                WriteLog(0, 0, "Connected to " + ddlReaderList.Text);//Successful connection to
            }

            GetUID();

            connActive = true;
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            if (connActive)
            {
                retCode = ModWinsCard.SCardDisconnect(hCard, ModWinsCard.SCARD_UNPOWER_CARD);
            }

            retCode = ModWinsCard.SCardReleaseContext(hCard);

            connActive = false;
        }

        private void frmStart_FormClosing(object sender, FormClosingEventArgs e)
        {
            retCode = ModWinsCard.SCardReleaseContext(hContext);
            retCode = ModWinsCard.SCardDisconnect(hCard, ModWinsCard.SCARD_UNPOWER_CARD);
            System.Environment.Exit(0);
        }

        #region ClearBuffers
        private void ClearBuffers()
        {
            long indx;

            for (indx = 0; indx < 16; indx++)
            {
                RecvBuff[indx] = 0;
                SendBuff[indx] = 0;
                SendBuffAll[indx] = 0;
            }
        }
        #endregion

        #region WriteLog
        /// <summary>
        /// Write Log and Data
        /// </summary>
        /// <param name="errType"></param>
        /// <param name="retVal"></param>
        /// <param name="PrintText"></param>
        private void WriteLog(int errType, int retVal, string PrintText)
        {
            switch (errType)
            {
                case 0:
                    txtMessage.SelectionColor = Color.Green;
                    break;
                case 1:
                    txtMessage.SelectionColor = Color.Red;
                    PrintText = ModWinsCard.GetScardErrMsg(retVal);
                    break;
                case 2:
                    txtMessage.SelectionColor = Color.Black;
                    PrintText = "<" + PrintText;
                    break;
                case 3:
                    txtMessage.SelectionColor = Color.Black;
                    PrintText = ">" + PrintText;
                    break;
                case 4:
                    break;
            }
            txtMessage.AppendText(PrintText);
            txtMessage.AppendText("\n");
            txtMessage.SelectionColor = Color.Black;
            txtMessage.Focus();
        }
        #endregion

        private void WriteDataIntoCard(string data)
        {
            if (!connActive) return;

            NdefTextRecord textRecord = new NdefTextRecord() { Text = data, TextEncoding = NdefTextRecord.TextEncodingType.Utf8 };
            NdefRecord record = new NdefRecord(textRecord);
            NdefMessage message = new NdefMessage { record };

            byte[] bytes = message.ToByteArray();

            int startBlockNum = 4, totalDataLength = bytes.Length + 3;
            int totalBlock = (totalDataLength % 4 == 0 ? (totalDataLength / 4) : (totalDataLength / 4 + 1));
            byte[] totalData = new byte[totalBlock * 4];
            int dataPageIndex = 0;

            totalData[0] = 0x03;
            totalData[1] = (byte)bytes.Length;

            for (int i = 0; i < bytes.Length; i++)
            {
                totalData[i + 2] = bytes[i];
            }
            totalData[totalData[1] + 2] = 0xFE;

            for (int iBlock = startBlockNum; iBlock < totalBlock + startBlockNum; iBlock++)
            {
                ClearBuffers();
                SendBuff[0] = 0xFF;         // CLA
                SendBuff[1] = 0xD6;         // INS
                SendBuff[2] = 0x00;         // P1
                SendBuff[3] = (byte)iBlock; // P2: Starting Block No.
                SendBuff[4] = 0x04;         // P3 : Data length

                for (int idx = 0; idx < 4; idx++)
                {
                    SendBuff[idx + 5] = totalData[(dataPageIndex * 4) + idx];
                }
                SendLen = 9;
                RecvLen = 0x02;

                retCode = SendAPDU(2);

                string tmpStr = string.Empty;
                if (retCode == ModWinsCard.SCARD_S_SUCCESS)
                {
                    tmpStr = "";
                    for (int idx = 0; idx <= RecvLen - 1; idx++)
                    {
                        tmpStr = tmpStr + string.Format("{0:X2}", RecvBuff[idx]);
                    }
                }
                if (tmpStr.Trim() == "9000")
                {

                }
                else
                {
                    WriteLog(4, 0, "Write block error!>>" + tmpStr.Trim());
                }

                dataPageIndex++;
            }
        }

        private byte[] ReadDataFromCard(int iBlock, int length)
        {
            byte[] returnData = new byte[6];

            if (connActive)
            {
                ClearBuffers();
                SendBuff[0] = 0xFF;
                SendBuff[1] = 0xB0;
                SendBuff[2] = 0x00;
                SendBuff[3] = (byte)iBlock;
                SendBuff[4] = (byte)length;

                SendLen = 5;
                RecvLen = SendBuff[4] + 2;

                retCode = SendAPDU(1);

                string tmpStr = string.Empty;
                if (retCode == ModWinsCard.SCARD_S_SUCCESS)
                {
                    tmpStr = "";
                    for (int idx = RecvLen - 2; idx <= RecvLen - 1; idx++)
                    {
                        tmpStr = tmpStr + string.Format("{0:X2}", RecvBuff[idx]);
                    }
                }
                if (tmpStr.Trim() == "9000")
                {
                    returnData = RecvBuff;
                }
                else
                {
                    WriteLog(4, 0, "Read block error!");
                }
            }

            return returnData;
        }

        private byte[] EraseData()
        {
            byte[] returnData = new byte[6];

            if (connActive)
            {
                ClearBuffers();
                SendBuff[0] = 0xFF;
                SendBuff[1] = 0xD6;
                SendBuff[2] = 0x00;
                SendBuff[3] = 0x04;
                SendBuff[4] = 0x04;
                SendBuff[5] = 0x03;
                SendBuff[6] = 0x00;
                SendBuff[7] = 0xFE;
                SendBuff[8] = 0x00;

                SendLen = 9;
                RecvLen = 0x02;

                retCode = SendAPDU(2);

                string tmpStr = string.Empty;
                if (retCode == ModWinsCard.SCARD_S_SUCCESS)
                {
                    tmpStr = "";
                    for (int idx = RecvLen - 2; idx <= RecvLen - 1; idx++)
                    {
                        tmpStr = tmpStr + string.Format("{0:X2}", RecvBuff[idx]);
                    }
                }
                if (tmpStr.Trim() == "9000")
                {
                    returnData = RecvBuff;
                }
                else
                {
                    WriteLog(4, 0, "Read block error!");
                }
            }

            return returnData;
        }

        private byte[] BlockCard()
        {
            byte[] returnData = new byte[6];

            byte[] block2Data = ReadDataFromCard(2, 4);

            if (connActive)
            {
                ClearBuffers();
                SendBuff[0] = 0xFF;
                SendBuff[1] = 0xD6;
                SendBuff[2] = 0x00;
                SendBuff[3] = 0x02;
                SendBuff[4] = 0x04;
                SendBuff[5] = block2Data[0];
                SendBuff[6] = block2Data[1];
                SendBuff[7] = 0xFF;
                SendBuff[8] = 0xFF;

                SendLen = 9;
                RecvLen = SendBuff[4] + 2;

                retCode = SendAPDU(2);

                string tmpStr = string.Empty;
                if (retCode == ModWinsCard.SCARD_S_SUCCESS)
                {
                    tmpStr = "";
                    for (int idx = RecvLen - 2; idx <= RecvLen - 1; idx++)
                    {
                        tmpStr = tmpStr + string.Format("{0:X2}", RecvBuff[idx]);
                    }
                }
                if (tmpStr.Trim() == "9000")
                {
                    returnData = RecvBuff;
                }
                else
                {
                    WriteLog(4, 0, "Write block error!");
                }
            }

            return returnData;
        }

        #region GetUID()
        /// <summary>
        /// GetUID
        /// </summary>
        private string GetUID()
        {
            string returnValue = string.Empty;
            //Get the firmaware version of the reader
            string tmpStr = string.Empty;
            int intIndx;
            ClearBuffers();

            SendBuff[0] = 0xFF;
            SendBuff[1] = 0xCA;
            SendBuff[2] = 0x00;
            SendBuff[3] = 0x00;
            SendBuff[4] = 0x00;
            SendLen = 5;
            RecvLen = 10;
            retCode = SendAPDU(1);
            if (retCode != ModWinsCard.SCARD_S_SUCCESS)
                return returnValue;

            // Interpret firmware data
            //tmpStr = "Firmware Version: ";
            for (intIndx = 0; intIndx <= RecvLen - 3; intIndx++)
            {
                tmpStr = tmpStr + string.Format("{0:X2}", RecvBuff[intIndx]);
            }
            returnValue = tmpStr;
            WriteLog(3, 0, tmpStr);

            return returnValue;
        }
        #endregion

        #region SendAPDU
        /// <summary>
        /// 
        /// </summary>
        /// <param name="handleFlag">isWriteOrNot</param>
        /// <returns></returns>
        public int SendAPDU(int handleFlag)
        {
            pioSendRequest.dwProtocol = Aprotocol;
            pioSendRequest.cbPciLength = 8;

            // Display Apdu In
            string tmpStr = string.Empty;
            if (handleFlag == 2)//Update
            {
                for (int idx = 0; idx <= 4; idx++)
                {
                    tmpStr = tmpStr + " " + string.Format("0x{0:X2}({0:D3})", SendBuff[idx]);
                }
            }
            else if (handleFlag == 1)//Read
            {
                for (int idx = 0; idx <= SendLen - 1; idx++)
                {
                    tmpStr = tmpStr + " " + string.Format("0x{0:X2}({0:D3})", SendBuff[idx]);
                }
            }
            else if (handleFlag == 0)//Format
            {
                for (int idx = 0; idx <= 4; idx++)
                {
                    tmpStr = tmpStr + " " + string.Format("0x{0:X2}({0:D3})", SendBuffAll[idx]);
                }
            }

            //WriteLog(2, 0, tmpStr);
            retCode = ModWinsCard.SCardTransmit(hCard, ref pioSendRequest, ref SendBuff[0], SendLen, ref pioSendRequest, ref RecvBuff[0], ref RecvLen);

            if (retCode != ModWinsCard.SCARD_S_SUCCESS)
            {
                WriteLog(1, retCode, "");
                return retCode;
            }

            tmpStr = "";
            for (int idx = 0; idx < RecvLen; idx++)
            {
                tmpStr = tmpStr + " " + string.Format("0x{0:X2}({0:D3})", RecvBuff[idx]);
            }

            WriteLog(3, 0, tmpStr);

            return retCode;
        }
        #endregion

        private void btnRead_Click(object sender, EventArgs e)
        {
            if (!connActive) return;

            string tmpStr = string.Empty;

            if (txtBlock.Text == "")
            {
                txtBlock.Focus();
                return;
            }

            if (int.Parse(txtBlock.Text) > 64)
            {
                txtBlock.Text = "64";
                return;
            }

            if (txtLength.Text == "")
            {
                txtLength.Focus();
                return;
            }

            byte[] recvData = ReadDataFromCard(int.Parse(txtBlock.Text), int.Parse(txtLength.Text));
            tmpStr = System.Text.Encoding.Default.GetString(recvData);

            WriteLog(3, 0, tmpStr);
        }

        private void btnReadAll_Click(object sender, EventArgs e)
        {
            if (!connActive) return;

            string tmpStr = string.Empty;
            int startBlock = 4, endBlock = 0, dataLength = 0, dataAllLength;
            int dataPageIndex = 0;
            byte[] recvData = new byte[6];
            recvData = ReadDataFromCard(startBlock, 4);

            dataLength = recvData[1];
            dataAllLength = dataLength + 3;

            if (dataLength > 0)
            {
                byte[] effectiveData = new byte[dataLength];

                effectiveData[0] = recvData[2];
                effectiveData[1] = recvData[3];

                endBlock = dataAllLength % 4 == 0 ? dataAllLength / 4 + startBlock : dataAllLength / 4 + startBlock + 1;

                for (int iBlock = startBlock + 1; iBlock < endBlock; iBlock++)
                {
                    recvData = ReadDataFromCard(iBlock, 4);

                    for (int iBit = 0; iBit < 4; iBit++)
                    {
                        if ((dataPageIndex * 4 + iBit + 2) < effectiveData.Length)
                            effectiveData[dataPageIndex * 4 + iBit + 2] = recvData[iBit];
                    }
                    dataPageIndex++;
                }

                try
                {
                    NdefMessage message = NdefMessage.FromByteArray(effectiveData);
                    NdefRecord record = message[0];

                    if (record.CheckSpecializedType(false) == typeof(NdefTextRecord))
                    {
                        //Convert and extract Smart Poster info
                        var textRecord = new NdefTextRecord(record);

                        WriteLog(3, 0, textRecord.Text);
                    }
                }
                catch
                {

                }
            }
        }

        private void btnWrite_Click(object sender, EventArgs e)
        {
            if (!connActive) return;

            string tmpStr = txtData.Text;

            if (tmpStr.Length > 0)
                WriteDataIntoCard(tmpStr);
        }

        private void btnWrteID_Click(object sender, EventArgs e)
        {
            if (!connActive) return;

            string tmpStr = GetUID();

            if (tmpStr.Length > 0)
                WriteDataIntoCard(tmpStr);
        }

        private void btnEraseData_Click(object sender, EventArgs e)
        {
            if (!connActive) return;

            byte[] recvData = EraseData();
        }

        private void btnBlockCard_Click(object sender, EventArgs e)
        {
            if (!connActive) return;

            if (MessageBox.Show("Cannot undo, lock card?", "Warning", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                byte[] recvData = BlockCard();
            }
        }
    }
}
namespace NTagReader
{
    partial class frmStart
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnInit = new System.Windows.Forms.Button();
            this.ddlReaderList = new System.Windows.Forms.ComboBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtMessage = new System.Windows.Forms.RichTextBox();
            this.btnReset = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtBlock = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtLength = new System.Windows.Forms.TextBox();
            this.btnRead = new System.Windows.Forms.Button();
            this.btnReadAll = new System.Windows.Forms.Button();
            this.txtData = new System.Windows.Forms.TextBox();
            this.btnWrite = new System.Windows.Forms.Button();
            this.btnWrteID = new System.Windows.Forms.Button();
            this.btnEraseData = new System.Windows.Forms.Button();
            this.btnBlockCard = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnInit
            // 
            this.btnInit.Location = new System.Drawing.Point(283, 10);
            this.btnInit.Margin = new System.Windows.Forms.Padding(4);
            this.btnInit.Name = "btnInit";
            this.btnInit.Size = new System.Drawing.Size(75, 28);
            this.btnInit.TabIndex = 1;
            this.btnInit.Text = "Init";
            this.btnInit.UseVisualStyleBackColor = true;
            this.btnInit.Click += new System.EventHandler(this.btnInit_Click);
            // 
            // ddlReaderList
            // 
            this.ddlReaderList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlReaderList.FormattingEnabled = true;
            this.ddlReaderList.Location = new System.Drawing.Point(98, 12);
            this.ddlReaderList.Name = "ddlReaderList";
            this.ddlReaderList.Size = new System.Drawing.Size(178, 24);
            this.ddlReaderList.TabIndex = 27;
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(365, 10);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(75, 28);
            this.btnConnect.TabIndex = 28;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 17);
            this.label1.TabIndex = 29;
            this.label1.Text = "Reader List";
            // 
            // txtMessage
            // 
            this.txtMessage.Font = new System.Drawing.Font("Microsoft New Tai Lue", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtMessage.Location = new System.Drawing.Point(446, 10);
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.Size = new System.Drawing.Size(628, 495);
            this.txtMessage.TabIndex = 30;
            this.txtMessage.Text = "";
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(365, 44);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(75, 28);
            this.btnReset.TabIndex = 31;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(50, 94);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 17);
            this.label2.TabIndex = 32;
            this.label2.Text = "Block";
            // 
            // txtBlock
            // 
            this.txtBlock.Location = new System.Drawing.Point(98, 91);
            this.txtBlock.Name = "txtBlock";
            this.txtBlock.Size = new System.Drawing.Size(100, 23);
            this.txtBlock.TabIndex = 33;
            this.txtBlock.Text = "4";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(204, 94);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 17);
            this.label3.TabIndex = 34;
            this.label3.Text = "Length";
            // 
            // txtLength
            // 
            this.txtLength.Location = new System.Drawing.Point(258, 91);
            this.txtLength.Name = "txtLength";
            this.txtLength.Size = new System.Drawing.Size(100, 23);
            this.txtLength.TabIndex = 35;
            this.txtLength.Text = "4";
            // 
            // btnRead
            // 
            this.btnRead.Location = new System.Drawing.Point(365, 88);
            this.btnRead.Name = "btnRead";
            this.btnRead.Size = new System.Drawing.Size(75, 28);
            this.btnRead.TabIndex = 36;
            this.btnRead.Text = "Read";
            this.btnRead.UseVisualStyleBackColor = true;
            this.btnRead.Click += new System.EventHandler(this.btnRead_Click);
            // 
            // btnReadAll
            // 
            this.btnReadAll.Location = new System.Drawing.Point(365, 122);
            this.btnReadAll.Name = "btnReadAll";
            this.btnReadAll.Size = new System.Drawing.Size(75, 28);
            this.btnReadAll.TabIndex = 36;
            this.btnReadAll.Text = "Read All";
            this.btnReadAll.UseVisualStyleBackColor = true;
            this.btnReadAll.Click += new System.EventHandler(this.btnReadAll_Click);
            // 
            // txtData
            // 
            this.txtData.Location = new System.Drawing.Point(98, 169);
            this.txtData.Multiline = true;
            this.txtData.Name = "txtData";
            this.txtData.Size = new System.Drawing.Size(260, 54);
            this.txtData.TabIndex = 37;
            // 
            // btnWrite
            // 
            this.btnWrite.Location = new System.Drawing.Point(365, 166);
            this.btnWrite.Name = "btnWrite";
            this.btnWrite.Size = new System.Drawing.Size(75, 28);
            this.btnWrite.TabIndex = 38;
            this.btnWrite.Text = "Write";
            this.btnWrite.UseVisualStyleBackColor = true;
            this.btnWrite.Click += new System.EventHandler(this.btnWrite_Click);
            // 
            // btnWrteID
            // 
            this.btnWrteID.Location = new System.Drawing.Point(365, 200);
            this.btnWrteID.Name = "btnWrteID";
            this.btnWrteID.Size = new System.Drawing.Size(75, 28);
            this.btnWrteID.TabIndex = 39;
            this.btnWrteID.Text = "Write ID";
            this.btnWrteID.UseVisualStyleBackColor = true;
            this.btnWrteID.Click += new System.EventHandler(this.btnWrteID_Click);
            // 
            // btnEraseData
            // 
            this.btnEraseData.BackColor = System.Drawing.Color.Red;
            this.btnEraseData.ForeColor = System.Drawing.SystemColors.Window;
            this.btnEraseData.Location = new System.Drawing.Point(339, 264);
            this.btnEraseData.Name = "btnEraseData";
            this.btnEraseData.Size = new System.Drawing.Size(101, 30);
            this.btnEraseData.TabIndex = 39;
            this.btnEraseData.Text = "Erase Data";
            this.btnEraseData.UseVisualStyleBackColor = false;
            this.btnEraseData.Click += new System.EventHandler(this.btnEraseData_Click);
            // 
            // btnBlockCard
            // 
            this.btnBlockCard.BackColor = System.Drawing.Color.Red;
            this.btnBlockCard.ForeColor = System.Drawing.SystemColors.Window;
            this.btnBlockCard.Location = new System.Drawing.Point(339, 300);
            this.btnBlockCard.Name = "btnBlockCard";
            this.btnBlockCard.Size = new System.Drawing.Size(101, 30);
            this.btnBlockCard.TabIndex = 39;
            this.btnBlockCard.Text = "Block Card";
            this.btnBlockCard.UseVisualStyleBackColor = false;
            this.btnBlockCard.Click += new System.EventHandler(this.btnBlockCard_Click);
            // 
            // frmStart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1086, 517);
            this.Controls.Add(this.btnBlockCard);
            this.Controls.Add(this.btnEraseData);
            this.Controls.Add(this.btnWrteID);
            this.Controls.Add(this.btnWrite);
            this.Controls.Add(this.txtData);
            this.Controls.Add(this.btnReadAll);
            this.Controls.Add(this.btnRead);
            this.Controls.Add(this.txtLength);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtBlock);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.txtMessage);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.ddlReaderList);
            this.Controls.Add(this.btnInit);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmStart";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "IDCardReader";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmStart_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnInit;
        internal System.Windows.Forms.ComboBox ddlReaderList;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox txtMessage;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtBlock;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtLength;
        private System.Windows.Forms.Button btnRead;
        private System.Windows.Forms.Button btnReadAll;
        private System.Windows.Forms.TextBox txtData;
        private System.Windows.Forms.Button btnWrite;
        private System.Windows.Forms.Button btnWrteID;
        private System.Windows.Forms.Button btnEraseData;
        private System.Windows.Forms.Button btnBlockCard;
    }
}


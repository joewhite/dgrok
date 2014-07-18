namespace DGrok.Demo
{
    partial class CompilerOptionsControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.edtDelphiVersionDefine = new System.Windows.Forms.TextBox();
            this.edtCustomDefines = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.edtCompilerOptionsSetOn = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.edtCompilerOptionsSetOff = new System.Windows.Forms.TextBox();
            this.edtTrueIfConditions = new System.Windows.Forms.TextBox();
            this.edtFalseIfConditions = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.edtParserThreadCount = new System.Windows.Forms.NumericUpDown();
            this.label13 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize) (this.edtParserThreadCount)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 84);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(129, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Compiler options set to \'+\':";
            // 
            // edtDelphiVersionDefine
            // 
            this.edtDelphiVersionDefine.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.edtDelphiVersionDefine.Location = new System.Drawing.Point(138, 3);
            this.edtDelphiVersionDefine.Name = "edtDelphiVersionDefine";
            this.edtDelphiVersionDefine.Size = new System.Drawing.Size(273, 20);
            this.edtDelphiVersionDefine.TabIndex = 1;
            this.edtDelphiVersionDefine.TextChanged += new System.EventHandler(this.edtDelphiVersionDefine_TextChanged);
            // 
            // edtCustomDefines
            // 
            this.edtCustomDefines.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.edtCustomDefines.Location = new System.Drawing.Point(138, 42);
            this.edtCustomDefines.Margin = new System.Windows.Forms.Padding(3, 6, 3, 0);
            this.edtCustomDefines.Name = "edtCustomDefines";
            this.edtCustomDefines.Size = new System.Drawing.Size(273, 20);
            this.edtCustomDefines.TabIndex = 2;
            this.edtCustomDefines.TextChanged += new System.EventHandler(this.edtCustomDefines_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 123);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(126, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Compiler options set to \'-\':";
            // 
            // edtCompilerOptionsSetOn
            // 
            this.edtCompilerOptionsSetOn.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.edtCompilerOptionsSetOn.Location = new System.Drawing.Point(138, 81);
            this.edtCompilerOptionsSetOn.Margin = new System.Windows.Forms.Padding(3, 6, 3, 0);
            this.edtCompilerOptionsSetOn.Name = "edtCompilerOptionsSetOn";
            this.edtCompilerOptionsSetOn.Size = new System.Drawing.Size(273, 20);
            this.edtCompilerOptionsSetOn.TabIndex = 4;
            this.edtCompilerOptionsSetOn.TextChanged += new System.EventHandler(this.edtCompilerOptionsSetOn_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 6);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(109, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Delphi version define:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 45);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(98, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Custom $DEFINEs:";
            // 
            // edtCompilerOptionsSetOff
            // 
            this.edtCompilerOptionsSetOff.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.edtCompilerOptionsSetOff.Location = new System.Drawing.Point(138, 120);
            this.edtCompilerOptionsSetOff.Margin = new System.Windows.Forms.Padding(3, 6, 3, 0);
            this.edtCompilerOptionsSetOff.Name = "edtCompilerOptionsSetOff";
            this.edtCompilerOptionsSetOff.Size = new System.Drawing.Size(273, 20);
            this.edtCompilerOptionsSetOff.TabIndex = 7;
            this.edtCompilerOptionsSetOff.TextChanged += new System.EventHandler(this.edtCompilerOptionsSetOff_TextChanged);
            // 
            // edtTrueIfConditions
            // 
            this.edtTrueIfConditions.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.edtTrueIfConditions.Location = new System.Drawing.Point(138, 159);
            this.edtTrueIfConditions.Margin = new System.Windows.Forms.Padding(3, 6, 3, 0);
            this.edtTrueIfConditions.Name = "edtTrueIfConditions";
            this.edtTrueIfConditions.Size = new System.Drawing.Size(273, 20);
            this.edtTrueIfConditions.TabIndex = 8;
            this.edtTrueIfConditions.TextChanged += new System.EventHandler(this.edtTrueIfConditions_TextChanged);
            // 
            // edtFalseIfConditions
            // 
            this.edtFalseIfConditions.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.edtFalseIfConditions.Location = new System.Drawing.Point(138, 198);
            this.edtFalseIfConditions.Margin = new System.Windows.Forms.Padding(3, 6, 3, 0);
            this.edtFalseIfConditions.Name = "edtFalseIfConditions";
            this.edtFalseIfConditions.Size = new System.Drawing.Size(273, 20);
            this.edtFalseIfConditions.TabIndex = 9;
            this.edtFalseIfConditions.TextChanged += new System.EventHandler(this.edtFalseIfConditions_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(2, 162);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(110, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "True $IF... conditions:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 201);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(113, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "False $IF... conditions:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(135, 23);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(93, 13);
            this.label7.TabIndex = 12;
            this.label7.Text = "Example: VER180";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(135, 62);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(100, 13);
            this.label8.TabIndex = 13;
            this.label8.Text = "Example: FOO;BAR";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(135, 101);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(72, 13);
            this.label9.TabIndex = 14;
            this.label9.Text = "Example: IRQ";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(135, 140);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(72, 13);
            this.label10.TabIndex = 15;
            this.label10.Text = "Example: IRQ";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(135, 179);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(165, 13);
            this.label11.TabIndex = 16;
            this.label11.Text = "Example: IF Foo or Bar;IF not Baz";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(135, 218);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(165, 13);
            this.label12.TabIndex = 17;
            this.label12.Text = "Example: IF Foo or Bar;IF not Baz";
            // 
            // edtParserThreadCount
            // 
            this.edtParserThreadCount.Location = new System.Drawing.Point(138, 237);
            this.edtParserThreadCount.Margin = new System.Windows.Forms.Padding(3, 6, 3, 0);
            this.edtParserThreadCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.edtParserThreadCount.Name = "edtParserThreadCount";
            this.edtParserThreadCount.Size = new System.Drawing.Size(64, 20);
            this.edtParserThreadCount.TabIndex = 18;
            this.edtParserThreadCount.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.edtParserThreadCount.ValueChanged += new System.EventHandler(this.edtParserThreadCount_ValueChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(2, 240);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(129, 13);
            this.label13.TabIndex = 19;
            this.label13.Text = "Number of parser threads:";
            // 
            // CompilerOptionsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.edtParserThreadCount);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.edtFalseIfConditions);
            this.Controls.Add(this.edtTrueIfConditions);
            this.Controls.Add(this.edtCompilerOptionsSetOff);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.edtCompilerOptionsSetOn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.edtCustomDefines);
            this.Controls.Add(this.edtDelphiVersionDefine);
            this.Controls.Add(this.label1);
            this.Name = "CompilerOptionsControl";
            this.Size = new System.Drawing.Size(414, 295);
            ((System.ComponentModel.ISupportInitialize) (this.edtParserThreadCount)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox edtDelphiVersionDefine;
        private System.Windows.Forms.TextBox edtCustomDefines;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox edtCompilerOptionsSetOn;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox edtCompilerOptionsSetOff;
        private System.Windows.Forms.TextBox edtTrueIfConditions;
        private System.Windows.Forms.TextBox edtFalseIfConditions;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.NumericUpDown edtParserThreadCount;
        private System.Windows.Forms.Label label13;
    }
}

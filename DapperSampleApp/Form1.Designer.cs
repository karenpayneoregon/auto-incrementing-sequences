namespace DapperSampleApp;

partial class Form1
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
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
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        CompanyListBox = new ListBox();
        CurrentSequenceLabel = new Label();
        IncrementCurrentSequenceButton = new Button();
        ResetCurrentButton = new Button();
        SuspendLayout();
        // 
        // CompanyListBox
        // 
        CompanyListBox.FormattingEnabled = true;
        CompanyListBox.Location = new Point(35, 12);
        CompanyListBox.Name = "CompanyListBox";
        CompanyListBox.Size = new Size(317, 224);
        CompanyListBox.TabIndex = 0;
        // 
        // CurrentSequenceLabel
        // 
        CurrentSequenceLabel.AutoSize = true;
        CurrentSequenceLabel.Location = new Point(374, 21);
        CurrentSequenceLabel.Margin = new Padding(0, 0, 3, 0);
        CurrentSequenceLabel.Name = "CurrentSequenceLabel";
        CurrentSequenceLabel.Size = new Size(57, 20);
        CurrentSequenceLabel.TabIndex = 1;
        CurrentSequenceLabel.Text = "Current";
        // 
        // IncrementCurrentSequenceButton
        // 
        IncrementCurrentSequenceButton.Location = new Point(35, 242);
        IncrementCurrentSequenceButton.Name = "IncrementCurrentSequenceButton";
        IncrementCurrentSequenceButton.Size = new Size(317, 29);
        IncrementCurrentSequenceButton.TabIndex = 2;
        IncrementCurrentSequenceButton.Text = "Increment current sequence";
        IncrementCurrentSequenceButton.UseVisualStyleBackColor = true;
        IncrementCurrentSequenceButton.Click += IncrementCurrentSequenceButton_Click;
        // 
        // ResetCurrentButton
        // 
        ResetCurrentButton.Location = new Point(35, 277);
        ResetCurrentButton.Name = "ResetCurrentButton";
        ResetCurrentButton.Size = new Size(317, 29);
        ResetCurrentButton.TabIndex = 3;
        ResetCurrentButton.Text = "Reset current sequence";
        ResetCurrentButton.UseVisualStyleBackColor = true;
        ResetCurrentButton.Click += ResetCurrentButton_Click;
        // 
        // Form1
        // 
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(543, 339);
        Controls.Add(ResetCurrentButton);
        Controls.Add(IncrementCurrentSequenceButton);
        Controls.Add(CurrentSequenceLabel);
        Controls.Add(CompanyListBox);
        FormBorderStyle = FormBorderStyle.FixedToolWindow;
        Name = "Form1";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Increment invoice code sample";
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private ListBox CompanyListBox;
    private Label CurrentSequenceLabel;
    private Button IncrementCurrentSequenceButton;
    private Button ResetCurrentButton;
}

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Breakout
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.ExitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ScoreLabel = New System.Windows.Forms.ToolStripMenuItem()
        Me.LivesLabel = New System.Windows.Forms.ToolStripMenuItem()
        Me.MainTimer = New System.Windows.Forms.Timer(Me.components)
        Me.AnimTimer = New System.Windows.Forms.Timer(Me.components)
        Me.StartGameTimer = New System.Windows.Forms.Timer(Me.components)
        Me.StartCountDown = New System.Windows.Forms.Label()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ExitToolStripMenuItem, Me.ScoreLabel, Me.LivesLabel})
        Me.MenuStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.MenuStrip1.Size = New System.Drawing.Size(384, 24)
        Me.MenuStrip1.TabIndex = 0
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'ExitToolStripMenuItem
        '
        Me.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem"
        Me.ExitToolStripMenuItem.Size = New System.Drawing.Size(37, 20)
        Me.ExitToolStripMenuItem.Text = "Exit"
        '
        'ScoreLabel
        '
        Me.ScoreLabel.Enabled = False
        Me.ScoreLabel.Name = "ScoreLabel"
        Me.ScoreLabel.Size = New System.Drawing.Size(48, 20)
        Me.ScoreLabel.Text = "Score"
        '
        'LivesLabel
        '
        Me.LivesLabel.Enabled = False
        Me.LivesLabel.Name = "LivesLabel"
        Me.LivesLabel.Size = New System.Drawing.Size(45, 20)
        Me.LivesLabel.Text = "Lives"
        '
        'MainTimer
        '
        Me.MainTimer.Interval = 30
        '
        'AnimTimer
        '
        Me.AnimTimer.Interval = 50
        '
        'StartGameTimer
        '
        Me.StartGameTimer.Interval = 1000
        '
        'StartCountDown
        '
        Me.StartCountDown.AutoSize = True
        Me.StartCountDown.Font = New System.Drawing.Font("Mistral", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.StartCountDown.Location = New System.Drawing.Point(167, 261)
        Me.StartCountDown.Name = "StartCountDown"
        Me.StartCountDown.Size = New System.Drawing.Size(0, 26)
        Me.StartCountDown.TabIndex = 1
        '
        'Breakout
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(384, 561)
        Me.Controls.Add(Me.StartCountDown)
        Me.Controls.Add(Me.MenuStrip1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "Breakout"
        Me.Text = "Breakout"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents ExitToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MainTimer As System.Windows.Forms.Timer
    Friend WithEvents AnimTimer As System.Windows.Forms.Timer
    Friend WithEvents ScoreLabel As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents LivesLabel As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents StartGameTimer As System.Windows.Forms.Timer
    Friend WithEvents StartCountDown As System.Windows.Forms.Label

End Class

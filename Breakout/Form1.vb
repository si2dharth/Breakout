Imports Microsoft.VisualBasic.PowerPacks

Public Class Breakout

    Const WINDOW_WIDTH As Integer = 400,
          WINDOW_HEIGHT As Integer = 600,
          BRICKS_PER_ROW As Integer = 10,
          NUM_ROWS As Integer = 10,
          TOP_OFFSET As Integer = 70,
          BRICK_SEP As Integer = 4,
          PADDLE_Y_OFFSET As Integer = 60,
          BRICK_HEIGHT As Integer = 10,
          BRICK_WIDTH As Integer = (WINDOW_WIDTH - (BRICKS_PER_ROW - 1) * BRICK_SEP) / BRICKS_PER_ROW,
          PADDLE_WIDTH As Integer = 60,
          PADDLE_HEIGHT As Integer = 10,
          BALL_RADIUS As Integer = 5,
          INITLIVES As Integer = 5,
          INITVY As Integer = -8,
          INITVX As Integer = 0,
          BRICKSCORE As Integer = 10,
          DAMPCOEF As Integer = PADDLE_WIDTH / 6

    Private WithEvents Canvas, MovingCanvas As ShapeContainer
    Private OriPos As Point
    Private Dragging As Boolean
    Private WithEvents paddle As RectangleShape
    Private WithEvents ball As OvalShape
    Private vx = INITVX, vy = INITVY

    Private _score As Integer
    Public Property Score() As Integer
        Get
            Return _score
        End Get
        Set(ByVal value As Integer)
            _score = value
            ScoreLabel.Text = "Score : " + Score.ToString
        End Set
    End Property

    Private _lives As Integer
    Public Property Lives() As Integer
        Get
            Return _lives
        End Get
        Set(ByVal value As Integer)
            _lives = value
            LivesLabel.Text = "Lives : " + Lives.ToString
        End Set
    End Property



    Private Sub Breakout_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Size = New Size(WINDOW_WIDTH, WINDOW_HEIGHT)
        Canvas = New ShapeContainer
        MovingCanvas = New ShapeContainer
        Controls.Add(MovingCanvas)
        Controls.Add(Canvas)
        Canvas.Enabled = False
        MovingCanvas.Enabled = False
        setEnvironment()
        makePaddle()
        makeBall()
        StartGameTimer.Enabled = True
        Score = 0
        Lives = INITLIVES
        StartCountDown.Location = New Point(WINDOW_WIDTH / 2 - StartCountDown.Width / 2, WINDOW_HEIGHT / 2 - StartCountDown.Height / 2)
        CountDown = 3
    End Sub

    Private Sub setEnvironment()
        Dim brick As RectangleShape
        For i = 1 To NUM_ROWS
            For j = 1 To BRICKS_PER_ROW
                brick = New RectangleShape
                brick.Location = New Point((j - 1) * (BRICK_WIDTH + BRICK_SEP), (i - 1) * (BRICK_HEIGHT + BRICK_SEP) + TOP_OFFSET)
                brick.Size = New Size(BRICK_WIDTH, BRICK_HEIGHT)
                Canvas.Shapes.Add(brick)
                brick.FillStyle = FillStyle.Solid
                Select Case Math.Floor((i - 1) / 2)
                    Case 0
                        brick.FillColor = Color.Red
                    Case 1
                        brick.FillColor = Color.Orange
                    Case 2
                        brick.FillColor = Color.Yellow
                    Case 3
                        brick.FillColor = Color.Green
                    Case 4
                        brick.FillColor = Color.Cyan
                End Select
                brick.BorderColor = brick.FillColor
                'brick.CornerRadius = BRICK_HEIGHT / 4
                'brick.Enabled = False
            Next
        Next
    End Sub

    Private Sub makePaddle()
        paddle = New RectangleShape
        paddle.Size = New Size(PADDLE_WIDTH, PADDLE_HEIGHT)
        paddle.Location = New Point((WINDOW_WIDTH - PADDLE_WIDTH) / 2, WINDOW_HEIGHT - PADDLE_Y_OFFSET)
        paddle.FillStyle = FillStyle.Solid
        paddle.CornerRadius = PADDLE_HEIGHT / 2
        MovingCanvas.Shapes.Add(paddle)
    End Sub

    Private Sub makeBall()
        ball = New OvalShape
        ball.Size = New Size(2 * BALL_RADIUS, 2 * BALL_RADIUS)
        ball.Location = New Point(paddle.Location.X + PADDLE_WIDTH / 2 - BALL_RADIUS, paddle.Location.Y - 2 * BALL_RADIUS)
        ball.Enabled = False
        MovingCanvas.Shapes.Add(ball)
        ball.FillStyle = FillStyle.Solid
        ball.FillColor = Color.White
    End Sub

    Private Sub MoveBall()
        Dim ballLoc = ball.Location
        ballLoc.Offset(vx, vy)
        ball.Location = ballLoc
    End Sub

    Private Sub MenuStrip1_MouseDown(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles MenuStrip1.MouseDown, MyBase.MouseDown
        OriPos = e.Location
        Dragging = True
    End Sub

    Private Sub MenuStrip1_MouseMove(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles MenuStrip1.MouseMove, MyBase.MouseMove
        If Not Dragging Then Return
        Me.Location = New Point(e.X - OriPos.X + Location.X, e.Y - OriPos.Y + Location.Y)
    End Sub

    Private Sub MenuStrip1_MouseUp(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles MenuStrip1.MouseUp, MyBase.MouseUp
        Dragging = False
    End Sub

    Private Sub ExitToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles ExitToolStripMenuItem.Click
        Me.Close()
    End Sub

    Private Sub MouseMoved(sender As Object, e As MouseEventArgs) Handles MyBase.MouseMove
        If Dragging Then Return

        Dim MouseX As Integer = e.X

        If e.X + PADDLE_WIDTH / 2 > WINDOW_WIDTH Then
            MouseX = WINDOW_WIDTH - PADDLE_WIDTH / 2
        ElseIf e.X - PADDLE_WIDTH / 2 < 0 Then
            MouseX = PADDLE_WIDTH / 2
        End If

        If Not IsNothing(MovingCanvas.GetChildAtPoint(New Point(ball.Location.X + BALL_RADIUS, ball.Location.Y + 2 * BALL_RADIUS + 1))) Then
            Dim ballLoc = ball.Location
            ballLoc.Offset(MouseX - paddle.Location.X - PADDLE_WIDTH / 2, 0)
            ball.Location = ballLoc
        End If
        paddle.Location = New Point(MouseX - PADDLE_WIDTH / 2, WINDOW_HEIGHT - PADDLE_Y_OFFSET)
    End Sub

    Private Sub MainTimer_Tick(sender As System.Object, e As System.EventArgs) Handles MainTimer.Tick
        MoveBall()
        CheckForCollision()
    End Sub

    Private Sub ReduceLife()
        Lives -= 1
        vx = INITVX
        vy = INITVY
        ball.Location = New Point(paddle.Location.X + PADDLE_WIDTH / 2 - BALL_RADIUS, paddle.Location.Y - 2 * BALL_RADIUS)
        If Lives = 0 Then
            GameOver()
        Else
            MainTimer.Enabled = False
            CountDown = 3
            StartGameTimer.Enabled = True
        End If
    End Sub

    Private Sub GameOver()
        MainTimer.Enabled = False
        If MsgBox("Game Over! Score : " + Score.ToString + ". Play Again ?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then
            Me.Close()
            Return
        End If
        Canvas.Shapes.Clear()
        setEnvironment()
        StartGameTimer.Enabled = True
        Score = 0
        Lives = INITLIVES
        vx = INITVX
        vy = INITVY
        CountDown = 3
    End Sub

    Private Sub CheckForCollision()
        If ball.Location.X + 2 * BALL_RADIUS > WINDOW_WIDTH Or ball.Location.X < 0 Then
            vx = -vx
            Return
        End If

        If ball.Location.Y < MenuStrip1.Height Then
            vy = -vy
            Return
        End If

        If ball.Location.Y + 2 * BALL_RADIUS > WINDOW_HEIGHT Then
            ReduceLife()
            Return
        End If

        If ball.Location.Y < PADDLE_Y_OFFSET And ball.Location.Y > TOP_OFFSET + (NUM_ROWS + 1) * (BRICK_HEIGHT + BRICK_SEP) Then
            Return
        End If

        Dim s As Shape

        s = MovingCanvas.GetChildAtPoint(New Point(ball.Location.X + BALL_RADIUS, ball.Location.Y + 2 * BALL_RADIUS + 1))
        If Not IsNothing(s) Then
            vx += (ball.Location.X + BALL_RADIUS - paddle.Location.X - PADDLE_WIDTH / 2) / DAMPCOEF
            vy = -vy
            Return
        End If

        CheckForBrickCollision()
    End Sub

    Private Sub CheckForBrickCollision()
        Dim s As Shape
        Dim s2 As Shape

        If ball.Location.Y > TOP_OFFSET + (NUM_ROWS + 1) * (BRICK_HEIGHT + BRICK_SEP) Then
            Return
        End If

        s = Canvas.GetChildAtPoint(New Point(ball.Location.X + BALL_RADIUS, ball.Location.Y))
        If Not IsNothing(s) Then
            vy = -vy
            CollidedTo(s)
            Return
        End If

        s = Canvas.GetChildAtPoint(New Point(ball.Location.X + BALL_RADIUS, ball.Location.Y + 2 * BALL_RADIUS))
        If Not IsNothing(s) Then
            vy = -vy
            CollidedTo(s)
            Return
        End If

        s = Canvas.GetChildAtPoint(New Point(ball.Location.X, ball.Location.Y + BALL_RADIUS))
        If Not IsNothing(s) Then
            s2 = Canvas.GetChildAtPoint(New Point(ball.Location.X + 2 * BALL_RADIUS, ball.Location.Y + BALL_RADIUS))
            If Not IsNothing(s2) Then
                vy = -vy
                CollidedTo(s2)
            Else
                vx = -vx
            End If
            CollidedTo(s)
            Return
        End If

        s = Canvas.GetChildAtPoint(New Point(ball.Location.X + 2 * BALL_RADIUS, ball.Location.Y + BALL_RADIUS))
        If Not IsNothing(s) Then
            vx = -vx
            CollidedTo(s)
            Return
        End If
    End Sub


    Private Sub CollidedTo(s As Shape)
        Canvas.Shapes.Remove(s)
        Score += BRICKSCORE
        'DisappearList.Add(s)
        's.Enabled = False
        'AnimTimer.Enabled = True
    End Sub
    'EXTRAS

    Private DisappearList As New List(Of RectangleShape)

    Private Sub AnimTimer_Tick(sender As System.Object, e As System.EventArgs) Handles AnimTimer.Tick
        If DisappearList.Count = 0 Then
            AnimTimer.Enabled = False
            Return
        End If

        Dim i As Integer

        While i < DisappearList.Count
            If DisappearList(i).Height <= 1 Then
                Canvas.Shapes.Remove(DisappearList(i))
                DisappearList.RemoveAt(i)
                If i >= DisappearList.Count Then Return
            End If

            DisappearList(i).Height -= 2
            'TmpLoc = DisappearList(i).Location
            'TmpLoc.Offset(1, 1)
            'DisappearList(i).Location = TmpLoc
            i += 1
        End While

    End Sub

    Private _CountDown As Integer
    Public Property CountDown() As Integer
        Get
            Return _CountDown
        End Get
        Set(ByVal value As Integer)
            StartCountDown.Visible = True
            _CountDown = value
            StartCountDown.Text = _CountDown
        End Set
    End Property


    Private Sub StartGameTimer_Tick(sender As System.Object, e As System.EventArgs) Handles StartGameTimer.Tick
        CountDown -= 1
        If CountDown = 0 Then
            StartGameTimer.Enabled = False
            StartCountDown.Visible = False
            MainTimer.Enabled = True
        End If
    End Sub
End Class

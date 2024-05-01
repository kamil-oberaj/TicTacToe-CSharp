using TicTacToe;

var player1 = StaticMembers.GetPlayer();
var player2 = StaticMembers.GetPlayer();
var currentPlayer = player1;
var currentProgress = false;

while (!currentProgress)
{
    StaticMembers.DisplayBoard();
    StaticMembers.GetPositionAndSeed(currentPlayer);
    currentProgress = StaticMembers.HasWon(currentPlayer);
    if (currentProgress) break;
    if (StaticMembers.IsBoardFull())
    {
        StaticMembers.OnDraw();
        return;
    }

    currentPlayer = currentPlayer == player1 ? player2 : player1;
}

StaticMembers.OnWin(currentPlayer);
<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false" CodeFile="QuizGame.aspx.cs" Inherits="QuizGame" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <title>Quiz Game</title>
    <link rel="icon" href="../images/favicon1.ico" type="image/ico" />

    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.4.1/jquery.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.7/umd/popper.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.3.1/js/bootstrap.min.js"></script>
    <script type="text/javascript">
        function Validate() {
            var rb = document.getElementById("<%=rbtnOptions.ClientID%>");
            var radio = rb.getElementsByTagName("input");
            var isChecked = false;
            for (var i = 0; i < radio.length; i++) {
                if (radio[i].checked) {
                    isChecked = true;
                    break;
                }
            }
            if (!isChecked) {
                alert("Please select an option!");
            }

            return isChecked;
        }
    </script>

</head>
<body>
    <form runat="server">
        <div class="container">
            <div class="container-fluid">
                <div class="row">

                    <div class="col-md-12 col-xs-12 col-sm-12 shadow-lg p-3 mb-5 bg-white rounded " style="margin-top:40px">
                        <div style="margin-top: 85px; margin-bottom: 105px;">
                            <h4>
                                <asp:Label ID="lblQuestion" runat="server" Style="text-align: justify" />
                            </h4>
                            <br />
                            <br />
                            <asp:RadioButtonList ID="rbtnOptions" runat="server">
                            </asp:RadioButtonList>
                            <br />


                            <br />
                            <br />
                            <asp:Button ID="btnnext" Text="Next" runat="server" class="btn btn-primary" OnClientClick="return Validate()" OnClick="Next" style="width:100px;float:right" />
                           

                            
                        </div>
                    </div>
                </div>


                <div id="dvResult" runat="server" visible="false" style="text-align:center">
                    <h1>
                        <asp:Label ID="lblResult" runat="server"  />
                    </h1>
                    <br />
                </div>
            </div>
        </div>
    </form>
</body>
</html>

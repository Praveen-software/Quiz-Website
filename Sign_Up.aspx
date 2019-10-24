<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false" CodeFile="Sign_Up.aspx.cs" Inherits="Sign_Up" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta http-equiv="X-UA-Compatible" content="ie=edge">
    <title>Play Your Game</title>

    <!-- Font Icon -->
    <link rel="stylesheet" href="fonts/material-icon/css/material-design-iconic-font.min.css">

    <!-- Main css -->
    <link rel="stylesheet" href="css/style.css">
    <script src="Scripts/jquery-1.7.1.min.js"></script>

    <script>
        function checkforblank() {
            var pgng = document.getElementById("<%=txtname.ClientID%>").value.trim();
            if (pgng == "") {
                alert('Please Enter Your  Name');
                return false;
            }

            pgng = document.getElementById("<%=txtmobileno.ClientID%>").value.trim();
            if (pgng == "") {
                alert('Please Enter Your Contact number');
                return false;
            }
            pgng = document.getElementById("<%=txtemail.ClientID%>").value.trim();
            if (pgng == "") {
                alert('Please Enter Your Email');
                return false;
            }

            pgng = document.getElementById("<%=txtpassword.ClientID%>").value.trim();
            if (pgng == "") {
                alert('Please Enter Your Password');
                return false;
            }
            pgng = document.getElementById("<%=ddlcollege.ClientID%>").value.trim();
            if (pgng == "") {
                alert('Please Select Your College');
                return false;
            }
        }
        function UserOrEmailAvailability() { //This function call on text change.             
            $.ajax({
                type: "POST",
                url: "Sign_Up.aspx/CheckEmail", // this for calling the web method function in cs code.  
                data: '{useroremail: "' + $("#<%=txtemail.ClientID%>")[0].value + '" }',// user name or email value  
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnSuccess,
                failure: function (response) {
                    alert(response);
                }
            });
        }

        // function OnSuccess  
        function OnSuccess(response) {
            var msg = $("#<%=Alert.ClientID%>")[0];
            switch (response.d) {
                case "true":
                    msg.style.display = "block";
                    msg.style.color = "red";
                    msg.innerHTML = "Email already exists.";
                    break;
                case "false":
                    msg.style.display = "block";
                    msg.style.color = "green";
                    msg.innerHTML = "Email Available";
                    break;
            }
        }

    </script>
</head>
<body>
    <form runat="server">
        <!-- Sign up form -->
        <div class="container">
            <div class="signup-content" style="padding: 20px 0;">


                <div class="signup-form">

                    <h2 class="form-title">Sign up</h2>
                    <form method="POST" class="register-form" id="register-form">
                        <div class="form-group">
                            <label for="name"><i class="zmdi zmdi-account material-icons-name"></i></label>
                            <asp:TextBox ID="txtname" runat="server" placeholder="Your Name"></asp:TextBox>

                        </div>
                        <div class="form-group">
                            <label for="email"><i class="zmdi zmdi-email"></i></label>
                            <asp:TextBox ID="txtemail" runat="server" placeholder="Your Email" onchange="UserOrEmailAvailability()"></asp:TextBox>

                        </div>
                        <div class="form-group">
                            <label for="pass"><i class="zmdi zmdi-lock"></i></label>
                            <asp:TextBox ID="txtpassword" runat="server" type="password" placeholder="Password"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label for="re-pass"><i class="zmdi zmdi-account material-icons-name"></i></label>
                            <asp:TextBox ID="txtmobileno" runat="server" placeholder="Your Contact No"></asp:TextBox>

                        </div>
                        <div class="form-group">
                            <asp:DropDownList ID="ddlcollege" runat="server" Style="width: 100%; display: block; border: none; border-bottom: 1px solid #999; padding: 6px 30px; font-family: sans-serif; box-sizing: border-box;">
                                <asp:ListItem Value="">Please Select Your College</asp:ListItem>
                                <asp:ListItem>New Delhi </asp:ListItem>
                                <asp:ListItem>Greater Noida</asp:ListItem>
                                <asp:ListItem>NewYork</asp:ListItem>
                                <asp:ListItem>Paris</asp:ListItem>
                                <asp:ListItem>London</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="form-group" id="Alert" runat="server" style="font-size: 18px">
                        </div>
                        <div class="form-group">
                            <input type="checkbox" name="agree-term" id="agree-term" class="agree-term" />
                            <label for="agree-term" class="label-agree-term"><span><span></span></span>I agree all statements in  <a href="#" class="term-service">Terms of service</a></label>
                        </div>
                        <div class="form-group form-button">
                            <asp:Button ID="signup" runat="server" class="form-submit" value="Register" Text="Registor" OnClick="signup_Click" OnClientClick="return checkforblank();" />

                        </div>
                    </form>
                </div>
                <div class="signup-image">
                    <figure>
                        <img src="images/signup-image.jpg" alt="sing up image">
                    </figure>
                    <a href="Login.aspx" class="signup-image-link">I am already member</a>
                </div>
            </div>
        </div>

        <!-- JS -->
        <script src="vendor/jquery/jquery.min.js"></script>
        <script src="js/main.js"></script>
    </form>
</body>
</html>

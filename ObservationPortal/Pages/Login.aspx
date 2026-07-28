<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="ObservationPortal.Pages.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>IT Audit Observation Portal </title>
    <link rel="stylesheet"
href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.11.3/font/bootstrap-icons.min.css" />

    <style>

        body{

    margin:0;
    padding:0;

    font-family:'Segoe UI',sans-serif;

    background:linear-gradient(135deg,#004C97,#0A74DA);

    height:100vh;

    display:flex;

    justify-content:center;

    align-items:center;

}

        .login-box{

    width:420px;

    background:white;

    padding:40px;

    border-radius:20px;

    box-shadow:0 15px 40px rgba(0,0,0,.25);

}

       h2{

    text-align:center;

    color:#004C97;

    margin-bottom:10px;

    font-weight:700;

}

        .txt{

    width:100%;

    padding:12px;

    margin-top:10px;

    margin-bottom:20px;

    border:1px solid #ccc;

    border-radius:8px;

    font-size:15px;

    box-sizing:border-box;

}

        .password-container{

    position:relative;

}

.password-box{

    padding-right:45px;

}

.password-container{
    position:relative;
}

.toggle-password{
    position:absolute;
    right:18px;
    top:50%;
    transform:translateY(-50%);
    cursor:pointer;
    font-size:20px;
    color:#666;
    user-select:none;
}

.eye-icon{

    position:absolute;

    right:15px;

    top:50%;

    transform:translateY(-50%);

    cursor:pointer;

    font-size:20px;

    color:#6c757d;

}

.eye-icon:hover{

    color:#0A74DA;

}


        .txt:focus{

    outline:none;

    border-color:#0A74DA;

    box-shadow:0 0 5px rgba(10,116,218,.4);

}

        .btn{

    width:100%;

    padding:14px;

    background:#004C97;

    color:white;

    border:none;

    border-radius:8px;

    cursor:pointer;

    font-size:17px;

    transition:.3s;

}

        .btn:hover{

    background:#0A74DA;

}

        .msg{

            color:red;
            text-align:center;

        }

        .logo{

    font-size:60px;

    text-align:center;

    margin-bottom:10px;

}

.subtitle{

    text-align:center;

    color:#666;

    margin-top:-5px;

    margin-bottom:25px;

    font-size:15px;

}

.welcome{

    text-align:center;

    color:#888;

    font-size:13px;

    margin-top:-15px;

    margin-bottom:25px;

}

/* Hide Edge's built-in password reveal button */
input::-ms-reveal,
input::-ms-clear{
    display:none;
}

input[type=password]::-ms-reveal{
    display:none;
}

.footer{

    margin-top:20px;

    text-align:center;

    color:#888;

    font-size:12px;

}

.captcha-box{

    background:#EAF4FF;

    border:2px solid #0A74DA;

    border-radius:10px;

    text-align:center;

    padding:12px;

    margin-bottom:15px;

}

.captcha-text{

    font-size:24px;

    font-weight:bold;

    color:#004C97;

    letter-spacing:2px;

}

.login-btn-container{
    margin-top:25px;
}

.btn{
    width:100%;
    display:block;
}

    </style>
   <script>

       function togglePassword() {

           var txt = document.getElementById('<%= txtPassword.ClientID %>');
           var icon = document.querySelector(".eye-icon i");

           if (txt.type === "password") {

               txt.type = "text";
               icon.className = "bi bi-eye-slash";

           }
           else {

               txt.type = "password";
               icon.className = "bi bi-eye";

           }

       }

   </script>
</head>

<body>

<form id="form1" 
    runat="server"
    DefaultButton="btnLogin">

<div class="login-box">

    <div class="logo">
    🏦
</div>

<h2>ABC Bank</h2>

<p class="subtitle">
    IT Audit Observation Portal 
</p>

    <p class="welcome">
    Secure Login for Authorized Users
</p>

<p style="text-align:center;">
    
</p>

<asp:TextBox ID="txtUserName" runat="server"
CssClass="txt"
placeholder="Username"></asp:TextBox>

<div class="password-container">

    <asp:TextBox ID="txtPassword"
        runat="server"
        CssClass="txt password-box"
        TextMode="Password"
        placeholder="Password">
    </asp:TextBox>

    <span class="toggle-password"
          onclick="togglePassword()">
        👁
    </span>

</div>

<label style="font-weight:600;color:#004C97;">
    Verification
</label>

<div class="captcha-box">

    <asp:Label ID="lblCaptcha"
        runat="server"
        CssClass="captcha-text">
    </asp:Label>

</div>

<asp:TextBox
    ID="txtCaptcha"
    runat="server"
    CssClass="txt"
    placeholder="Enter Answer">
</asp:TextBox>

<div class="login-btn-container">

    <asp:Button
        ID="btnLogin"
        runat="server"
        Text="🔒 Secure Login"
        CssClass="btn"
        OnClick="btnLogin_Click" />

</div>

<br />

<asp:Label
    ID="lblMessage"
    runat="server"
    CssClass="msg">
</asp:Label>

<div class="footer">
    © 2026 ABC Bank. All Rights Reserved.
</div>

</div>   

</form>

</body>
</html>
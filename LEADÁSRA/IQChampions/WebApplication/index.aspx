<%@ Page Title="" Language="C#" MasterPageFile="~/iqMaster.Master" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="WebApplication.index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        <asp:Literal ID="jsLiteral" runat="server"></asp:Literal>
    </script>
</asp:Content>
<asp:Content ID="portletPlaceIndex" ContentPlaceHolderID="portletPlace" runat="server">
    <asp:Panel runat="server" ID="loginPortlet" Visible="true" CssClass="loginPortlet">
        <asp:Panel runat="server" ID="onBeforeLogin">
            <h5>Bejelentkezés</h5>
            <table>
                <tr>
                    <td>Név:</td>
                    <td>
                        <asp:TextBox runat="server" ID="loginPortletUname"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>Jelszó:</td>
                    <td>
                        <asp:TextBox runat="server" ID="loginPortletPass" TextMode="Password"></asp:TextBox></td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <asp:Button runat="server" ID="submitLoginPortlet" Text="Bejelntkezés" CausesValidation="false" /></td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel runat="server" ID="logoffPanel" Visible ="false">
            Üdvözöljük, <asp:Label runat="server" ID="nameLabelLogOff"></asp:Label>
            <asp:LinkButton runat="server" ID="logOffButton" Text="Kijelentkezés" OnClick="logOffButton_Click"></asp:LinkButton>
        </asp:Panel>
    </asp:Panel>
    <br />
     <asp:Panel runat="server" ID="top5" Visible ="true" CssClass="loginPortlet">
            <h5>Legjobb 5 játékos</h5>
            <asp:Literal runat="server" ID="lbTop5"></asp:Literal>
        </asp:Panel>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel runat="server" ID="tbThankYaou" Visible="false">
        <h3>
            <asp:Label runat="server" ID="lbThankYouMessage"></asp:Label></h3>
        <asp:HyperLink runat="server" ID="hlBackToMain" NavigateUrl="~/index.aspx">Vissza a főoldalra</asp:HyperLink>
    </asp:Panel>
    <asp:Panel ID="pnRegistration" runat="server" Visible="false">
        <h3>Regisztráció</h3>
        <table>
            <tr>
                <td>Felhasználó név:</td>
                <td>
                    <asp:TextBox ID="lbUsrName_reg" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="lbUsrName_reg" ForeColor="Red" Display="Dynamic" Text="Kötelező kitölteni"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>Jelszó:</td>
                <td>
                    <asp:TextBox ID="lbPass1_reg" runat="server" TextMode="Password"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="lbPass1_reg" ForeColor="Red" Display="Dynamic" Text="Kötelező kitölteni"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>Jelszó még egyszer:</td>
                <td>
                    <asp:TextBox ID="lbPass2_reg" runat="server" TextMode="Password"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="lbPass2_reg" ForeColor="Red" Display="Dynamic" Text="Kötelező kitölteni">
                    </asp:RequiredFieldValidator>
                    <asp:CompareValidator runat="server" ControlToCompare="lbPass1_reg" ControlToValidate="lbPass2_reg" ForeColor="Red" Display="Dynamic" Text="A két jelszónak egyeznie kell!">
                    </asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td>Email cím:</td>
                <td>
                    <asp:TextBox ID="tbEmail" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ID="rfvTbEmail" ForeColor="Red" Text="Kötelező kitölteni!" Display="Dynamic" ControlToValidate="tbEmail"></asp:RequiredFieldValidator>
                </td>
            </tr>
        </table>
        <asp:Button ID="btnRegister" runat="server" Text="Regisztráció" OnClick="btnRegister_Click" />
    </asp:Panel>

    <asp:Panel runat="server" ID="statistics" Visible="false">
        <h3>Játékstatisztika</h3>
        <asp:Literal runat="server" ID="litStatistics"></asp:Literal>
    </asp:Panel>
</asp:Content>

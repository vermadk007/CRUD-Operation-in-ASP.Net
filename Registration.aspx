<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Registration.aspx.cs" Inherits="CRUD.Registration" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table>
            <tr>
                <td>
                    Name :
                </td>
                <td>
                    <asp:TextBox ID="txtname" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Gender :
                </td>
                <td>
                    <asp:RadioButtonList ID="rblgender" runat="server" RepeatColumns="3">
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td>
                    Qualification :
                </td>
                <td>
                    <asp:DropDownList ID="ddlqualification" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    Hobbies :
                </td>
                <td>
                    <asp:CheckBoxList ID="cblhobbies" runat="server" RepeatColumns="3">
                    </asp:CheckBoxList>
                </td>
            </tr>
            <tr>
                <td>
                    Files :
                </td>
                <td>
                    <asp:FileUpload ID="fufile" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <asp:Button ID="Btnsave" runat="server" Text="Save" OnClick="Btnsave_Click" />
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <asp:GridView ID="grd" runat="server" AutoGenerateColumns="false" 
                        onrowcommand="grd_RowCommand">
                        <Columns>
                            <asp:TemplateField HeaderText="Name">
                                <ItemTemplate>
                                    <%#Eval("Name") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Qualification">
                                <ItemTemplate>
                                    <%#Eval("QName")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Gender">
                                <ItemTemplate>
                                    <%#Eval("GName")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Hobbies">
                                <ItemTemplate>
                                    <%#Eval("Hobbies")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Files">
                                <ItemTemplate>
                                    <%#Eval("Files")%>
                                </ItemTemplate>
                            </asp:TemplateField>

                             <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkbtnedit" runat="server" Text="Edit" CommandArgument='<%#Eval("RID") %>' CommandName="EDT"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>

                             <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkbtndelete" runat="server" Text="Delete" CommandArgument='<%#Eval("RID") %>' CommandName="DLT"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>

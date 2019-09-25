using Microsoft.AspNet.Identity;
using System;
using System.Linq;
using System.Web.UI;
using WebSiteLibreria;

public partial class Account_Register : Page
{
    protected void CreateUser_Click(object sender, EventArgs e)
    {
        //    var manager = new UserManager();
        //    var user = new ApplicationUser() { UserName = TextBoxUserName.Text };
        //    IdentityResult result = manager.Create(user, TextBoxPassword.Text);
        //    if (result.Succeeded)
        //    {
        //        IdentityHelper.SignIn(manager, user, isPersistent: false);
        //        IdentityHelper.RedirectToReturnUrl(Request.QueryString["ReturnUrl"], Response);
        //    }
        //    else
        //    {
        //        ErrorMessage.Text = result.Errors.FirstOrDefault();
        //    }        
    }

    protected void Cancelar_Click(object sender, EventArgs e)
    {
        //IdentityHelper.RedirectToReturnUrl(Request.QueryString["ReturnUrl"], Response);
    }
}
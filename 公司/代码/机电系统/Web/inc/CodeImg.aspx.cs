using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using WebUtility;

public partial class inc_CodeImg : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        //调用函数将验证码生成图片
        this.CreateCheckCodeImage(ValidateImage.GenerateRandomCode());
    }

    private void CreateCheckCodeImage(string checkCode)
    {  
        //记录生成的验证码
        Session["CheckCode"] = checkCode;
   
        //并向网页输出验证码图片
        ValidateImage validateImage = new ValidateImage(checkCode, 100, 30);
        validateImage.Image.Save(Response.OutputStream, System.Drawing.Imaging.ImageFormat.Gif);
    }
}


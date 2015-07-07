using Ivony.Html.Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ivony.Html;
using System.Text;

namespace WebJumony.CodeJumony
{
    public partial class CodeTransform : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //txtOriginalHtml.Text = @"<html><body><input id='list' class='hasvalue' name='list' style='width: 100%'type='text'></body></html>";
                //<html><body><input id="list" class="hasvalue" name="list" style="width: 100%" type="text"></body></html>
            }
        }

        protected void btnTransform_Click(object sender, EventArgs e)
        {
            var t = new LemonHtmlTransformer();
            t.OldHtmlStr = txtOriginalHtml.Text;
            txtNewHtml.Text = t.Transform();
        }

        
    }

    public class LemonHtmlTransformer
    {
        private static string[] AttrNameToIgnore = { "name", "type" };
        public string OldHtmlStr { get; set; }
        private JumonyParser _parser;

        public JumonyParser Parser
        {
            get { return _parser ?? (_parser = new JumonyParser()); }
        }


        public string Transform()
        {
            if (string.IsNullOrEmpty(OldHtmlStr))
            {
                return "";
            }

            var document = Parser.Parse(OldHtmlStr);

            TurnInputToTexbox(document);
            TurnSelectToDdl(document);

            return document.Render(); 
        }

        private void TurnSelectToDdl(IHtmlDocument document)
        {
            var selects = document.Find("select").ToList();

            foreach (var sElem in selects)
            {
                List<string> tmp = new List<string>();
                foreach (var attr in sElem.Attributes())
                {
                    if (AttrNameToIgnore.Contains(attr.Name.ToLower()))
                    {
                        continue;
                    }

                    tmp.Add(attr.Name + "=\"" + attr.AttributeValue + "\"");
                }

                StringBuilder sb = new StringBuilder();
                foreach (var node in sElem.Nodes())
                {
                    sb.AppendFormat("<asp:ListItem>{0}</asp:ListItem>", node.InnerText());

                    //node.InnerText;
                }

                sElem.ReplaceWith(string.Format("<asp:DropDownList {0} runat=\"server\">{1}</asp:DropDownList>",
                    string.Join(" ", tmp), sb.ToString()));
            }


        }

        private void TurnInputToTexbox(IHtmlDocument document)
        {
            var inputs = document.Find("input[type=text]").ToList();

            foreach (var node in inputs)
            {
                List<string> tmp = new List<string>();
                foreach (var attr in node.Attributes())
                {
                    if (AttrNameToIgnore.Contains(attr.Name.ToLower()))
                    {
                        continue;
                    }

                    tmp.Add(attr.Name + "=\"" + attr.AttributeValue + "\"");
                }
                node.ReplaceWith(string.Format("<asp:TextBox {0} runat=\"server\"></asp:TextBox>",
                    string.Join(" ", tmp)));
            }
        }

    }
}
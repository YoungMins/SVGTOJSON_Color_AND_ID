using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace SVGToJson
{
    public partial class SVGToJsonForm : Form
    {
        public SVGToJsonForm()
        {
            InitializeComponent();
        }

        string path = string.Empty;

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "SVG files (*.SVG)|*.svg|All files (*.*)|*.*";
            dlg.RestoreDirectory = true;

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                path = dlg.FileName;
                txtPath.Text = path;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(path != string.Empty)
            {
                if( File.Exists(path) )
                {
                    SaveFileDialog dlg = new SaveFileDialog();
                    dlg.Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*";
                    dlg.RestoreDirectory = true;

                    if ( dlg.ShowDialog() == DialogResult.OK)
                    {
                        DataStructure ds = new DataStructure();

                        List<NationColorCode> lstNationColorCodes = new List<NationColorCode>();

                        string text = File.ReadAllText(path);
                        HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();
                        document.Load(path);

                        HtmlNode node = document.DocumentNode.ChildNodes[4];

                        for (int i = 0; i < node.ChildNodes.Count; i++)
                        {
                            if (node.ChildNodes[i].Name.Equals("g"))
                            {
                                if(node.ChildNodes[i].ChildNodes["path"] != null)
                                {
                                    NationColorCode ncc = new NationColorCode();

                                    ncc.Name = node.ChildNodes[i].Attributes["id"].Value;
                                    ncc.ColorCode = node.ChildNodes[i].ChildNodes["path"].Attributes["style"].Value;
                                    ncc.ColorCode = ncc.ColorCode.Split('(')[1].Replace(")", "").Replace(";","");

                                    lstNationColorCodes.Add(ncc);
                                }                                
                            }
                        }

                        ds.NationColorCodes = lstNationColorCodes;

                        string str = JsonConvert.SerializeObject(ds);

                        File.WriteAllText(dlg.FileName, str);

                        MessageBox.Show("저장되었습니다!");
                    }
                }
                else
                {
                    MessageBox.Show("해당파일이 존재하지 않습니다!");
                }
            }
        }
    }
}

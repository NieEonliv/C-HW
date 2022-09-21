using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows;
using System.Reflection;
using System.Runtime.Remoting.Messaging;

namespace DEExam
{
    static class ControlerDek
    {
        private const int LIMITED_ITEMS_PAGE = 15;
        private static List<Material> materials;
        public static List<TextBlock> Listens;

        public static List<Material> Materials 
        {
            get { return materials; }
            set 
            { 
                materials = value;
                float temp = (float)materials.Count / (float)LIMITED_ITEMS_PAGE;
                maxPage = (int)Math.Round(temp,MidpointRounding.AwayFromZero);
            }
        }
        public static int currentPage = 1;
        public static int maxPage;

        public static void NumberPage(string block)
        {
            foreach (TextBlock item in Listens)
            {
                if (item.Text == block)
                    item.TextDecorations = TextDecorations.Underline;
                else
                    item.TextDecorations = null;
            }
            currentPage = int.Parse(block);
        }
        public static void leftPage()
        {
            if (currentPage == 1)
                return;
            currentPage -= 1;
            UnderlineChekc(true);
        }
        public static void RightPage()
        {
            if (maxPage < currentPage + 1)
                return;
            currentPage += 1;
            UnderlineChekc(false);
        }
           
        private static void UnderlineChekc(bool left)
        {
            for (int i = 0; i < Listens.Count; i++)
            {
                if (Listens[i].TextDecorations == TextDecorations.Underline)
                {
                    Listens[i].TextDecorations = null;
                    if (left)
                    {
                        if (int.Parse(Listens[0].Text) > 1)
                        {
                            MoveListens(left);
                            Listens[i].TextDecorations = TextDecorations.Underline;
                            break;
                        }
                        if (i - 1 < 0)
                            break;
                        MoveListens(left);
                        Listens[i - 1].TextDecorations = TextDecorations.Underline;
                        break;
                    }
                    else
                    {
                        if (int.Parse(Listens[Listens.Count - 1].Text) != maxPage)
                        {
                            MoveListens(left);
                            Listens[i].TextDecorations = TextDecorations.Underline;
                            break;
                        }
                        if (i + 1 >= Listens.Count)
                            break;

                        MoveListens(left);
                        Listens[i + 1].TextDecorations = TextDecorations.Underline;
                        break;
                    }
                }
            }           
        }

        private static void MoveListens(bool left)
        {
            if (left)
            {
                foreach (var item in Listens)
                {
                    if (item.Text != "1")
                        item.Text = (int.Parse(item.Text) - 1).ToString();
                    else
                        break;
                }
            }
            else
            {
                foreach (var item in Listens)
                {
                    if (Listens[Listens.Count - 1].Text != maxPage.ToString())
                        item.Text = (int.Parse(item.Text) + 1).ToString();
                    else
                        break;
                }
            }          
        }

        public static List<PanelInfo> CreateItems()
        {
            int endIndex = currentPage * LIMITED_ITEMS_PAGE;
            int startIndex = (currentPage - 1) * LIMITED_ITEMS_PAGE;

            List<PanelInfo> panelInfoList = new List<PanelInfo>();
            for (int i = startIndex; i < endIndex && i < materials.Count; i++)
                panelInfoList.Add(new PanelInfo() { DataContext = materials[i] });
            return panelInfoList;
        }
    }
}

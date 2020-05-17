using HtmlAgilityPack;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace StockAllInfomation
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : Window
    {
        Dictionary<string, int> dictAssetsField;
        Dictionary<string, List<string>> dictAssetsFieldGroup;
        Dictionary<string, List<string>> dictIncomeFieldGroup;
        Dictionary<string, int> dictSession;
        Dictionary<string, int> dictYear;
        Dictionary<string, int> dictMonth;
        Dictionary<int, string> dictStock;
        string headDir;
        string saveDir;
        Model1Container db;
        public MainWindow()
        {
            InitializeComponent();
            //db = new Model1Container();
            this.Closed += MainWindow_Closed;
            headDir = @"C:\Users\axray\OneDrive\StockWeb\";
            saveDir = @"D:\stock\";
            if (File.Exists("customStock.txt"))
            {
                txtbox_custom.Text = File.ReadAllText("customStock.txt");
            }
        }

        private void MainWindow_Closed(object sender, EventArgs e)
        {
            //db.Dispose();
        }

        private void initialAssetsFieldsGroup()
        {
            dictAssetsFieldGroup = new Dictionary<string, List<string>>();
            string[] groups = { "流動資產", "非流動資產", "資產總額", "流動負債", "非流動負債", "負債總額", "歸屬於母公司業主之權益", "股本", "資本公積", "保留盈餘", "其他權益", "歸屬於母公司業主之權益合計", "非控制權益", "權益總額", "待註銷股本股數（單位：股）", "預收股款（權益項下）之約當發行股數（單位：股）", "母公司暨子公司所持有之母公司庫藏股股數（單位：股）" };
            for (int i = 0; i < groups.Length; i++)
            {
                dictAssetsFieldGroup.Add(groups[i], new List<string>());
            }
        }

        private void initialIncomeFieldsGroup()
        {
            dictAssetsFieldGroup = new Dictionary<string, List<string>>();
            string[] groups = { "流動資產", "非流動資產", "資產總額", "流動負債", "非流動負債", "負債總額", "歸屬於母公司業主之權益", "股本", "資本公積", "保留盈餘", "其他權益", "歸屬於母公司業主之權益合計", "非控制權益", "權益總額", "待註銷股本股數（單位：股）", "預收股款（權益項下）之約當發行股數（單位：股）", "母公司暨子公司所持有之母公司庫藏股股數（單位：股）" };
            for (int i = 0; i < groups.Length; i++)
            {
                dictAssetsFieldGroup.Add(groups[i], new List<string>());
            }
        }

        private void loopFileGetStockNumber(string path)
        {
            dictStock = new Dictionary<int, string>();
            string[] strSplit;
            UTF8Encoding encoder = new UTF8Encoding();
            string utf8ReturnString;
            byte[] bytes;
            using (var reader = new StreamReader(path, System.Text.Encoding.GetEncoding("Big5")))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    if (line != string.Empty)
                    {
                        strSplit = line.Split(',');
                        bytes = Encoding.UTF8.GetBytes(strSplit[1]);
                        utf8ReturnString = encoder.GetString(bytes);
                        dictStock.Add(int.Parse(strSplit[0]), utf8ReturnString);
                    }
                }
            }
        }

        private void getBalanceFields(string path, string stockNum)
        {
            List<List<string>> table;
            string htmlText;
            string htmlStr;
            HtmlDocument doc1;
            string fieldGroup = string.Empty;
            foreach (string session in dictSession.Keys)
            {
                htmlText = File.ReadAllText(path +
                    @"\balance_" + session + ".html");
                doc1 = new HtmlAgilityPack.HtmlDocument();
                if (!htmlText.Contains("查無所需資料"))
                {
                    doc1.LoadHtml(htmlText);
                    table = doc1.DocumentNode.SelectSingleNode("//table[@class='hasBorder']")
                       .Descendants("tr").Where(tr => tr.Elements("td").Count() > 1).Select(tr => tr.Elements("td").Select(td => td.InnerText.Trim()).ToList()).ToList();
                    foreach (List<string> str in table)
                    {
                        htmlStr = WebUtility.HtmlDecode(str[0]).Trim();
                        htmlStr = transferBalanceFieldGroup(htmlStr);
                        htmlStr = transferBalanceField(htmlStr);
                        if (dictAssetsFieldGroup.ContainsKey(htmlStr))
                            fieldGroup = htmlStr;
                        if (fieldGroup == string.Empty)
                            continue;
                        htmlStr = WebUtility.HtmlDecode(str[0]).Trim();
                        htmlStr = transferBalanceFieldGroup(htmlStr);
                        htmlStr = transferBalanceField(htmlStr);
                        if (!dictAssetsFieldGroup[fieldGroup].Contains(htmlStr))
                            dictAssetsFieldGroup[fieldGroup].Add(htmlStr);
                    }
                }
            }
        }

        private void getIncomeFields(string path, string stockNum)
        {
            List<List<string>> table;
            string htmlText;
            string htmlStr;
            HtmlDocument doc1;
            string fieldGroup = string.Empty;
            foreach (string session in dictSession.Keys)
            {
                htmlText = File.ReadAllText(path +
                    @"\balance_" + session + ".html");
                doc1 = new HtmlAgilityPack.HtmlDocument();
                if (!htmlText.Contains("查無所需資料"))
                {
                    doc1.LoadHtml(htmlText);
                    table = doc1.DocumentNode.SelectSingleNode("//table[@class='hasBorder']")
                       .Descendants("tr").Where(tr => tr.Elements("td").Count() > 1).Select(tr => tr.Elements("td").Select(td => td.InnerText.Trim()).ToList()).ToList();
                    foreach (List<string> str in table)
                    {
                        htmlStr = WebUtility.HtmlDecode(str[0]).Trim();
                        htmlStr = transferBalanceFieldGroup(htmlStr);
                        htmlStr = transferBalanceField(htmlStr);
                        if (dictAssetsFieldGroup.ContainsKey(htmlStr))
                            fieldGroup = htmlStr;
                        if (fieldGroup == string.Empty)
                            continue;
                        htmlStr = WebUtility.HtmlDecode(str[0]).Trim();
                        htmlStr = transferBalanceFieldGroup(htmlStr);
                        htmlStr = transferBalanceField(htmlStr);
                        if (!dictAssetsFieldGroup[fieldGroup].Contains(htmlStr))
                            dictAssetsFieldGroup[fieldGroup].Add(htmlStr);
                    }
                }
            }
        }

        private string transferBalanceFieldGroup(string field)
        {
            switch (field)
            {
                case "資產合計":
                case "資產總計":
                    return "資產總額";
                case "負債合計":
                case "負債總計":
                    return "負債總額";
                case "權益總計":
                case "權益合計":
                    return "權益總額";
                case "負債及權益合計":
                    return "負債及權益總計";
                default:
                    return field;
            }
        }

        private string transferBalanceField(string field)
        {
            switch (field)
            {
                case "應收票據淨額":
                    return "應收票據";
                case "應收帳款淨額":
                case "應收款項淨額":
                    return "應收帳款";
                case "應收帳款－關係人淨額":
                case "應收款項－關係人":
                    return "應收帳款－關係人";
                case "其他應收款淨額":
                    return "其他應收款";
                case "其他應收款－關係人淨額":
                    return "其他應收款－關係人";
                case "本期所得稅負債":
                    return "當期所得稅負債";
                case "無活絡市場之債務工具投資－流動淨額":
                    return "無活絡市場之債券投資－流動淨額";
                case "無活絡市場之債務工具投資－非流動淨額":
                    return "無活絡市場之債券投資－非流動淨額";
                case "本期所得稅資產":
                    return "當期所得稅資產";
                case "待出售非流動資產（或處分群組）淨額":
                    return "待出售非流動資產（淨額）";
                case "與待出售非流動資產直接相關（或處分群組）之負債":
                    return "與待出售非流動資產直接相關之負債";
                case "與待出售非流動資產（或處分群組）直接相關之權益":
                    return "與待出售非流動資產直接相關之權益";
                case "待分配予業主之非流動資產（或處分群組）淨額益":
                    return "與待分配予業主之非流動資產（或處分群組）直接相關之權益";
                case "未分配盈餘（待彌補虧損）":
                    return "未分配盈餘（或待彌補虧損）";
                case "備供出售金融資產未實利益（損失）":
                    return "備供出售金融資產未實現損益";
                case "融券存入保證價款":
                    return "融券保證金";
                case "以成本衡量之金融資產－流動":
                    return "以成本衡量之金融資產－流動淨額";
                case "待分配股票股利（增資準備）":
                    return "待分配股票股利";
                case "無活絡市場之債務工具投資－非流動":
                    return "無活絡市場之債券投資－非流動淨額";
                case "不動產及設備合計":
                    return "不動產、廠房及設備";
                case "投資性不動產合計":
                    return "投資性不動產淨額";
                case "其他非流動資產合計":
                    return "其他非流動資產";
                case "資本公積":
                    return "資本公積合計";
                case "資本公積-採用權益法認列關聯企業及合資取得或處分子公司股權價格與帳面價值差額":
                    return "資本公積－採用權益法認列關聯企業及合資取得或處分子公司股權價格與帳面價值差額";
                case "保留盈餘（或累積虧損）合計":
                    return "保留盈餘合計";
                default:
                    return field;
            }
        }

        #region Process And Create Banlance Field csv
        private async Task<int> processBalanceHtml(string path)
        {
            loopFileGetStockNumber(path);
            await Task.Run(() =>
            {
                foreach (KeyValuePair<int, string> pair in dictStock)
                {
                    if (pair.Key != 6005 && pair.Key != 6024)
                        getBalanceFields(path, pair.Key.ToString());
                }
            });
            using (System.IO.StreamWriter file =
                   new System.IO.StreamWriter(headDir + @"stock\banalanceField.csv", true, System.Text.Encoding.GetEncoding("Big5")))
            {
                foreach (KeyValuePair<string, List<string>> pair in dictAssetsFieldGroup)
                {
                    file.WriteLine(pair.Key);
                    foreach (string val in pair.Value)
                        file.WriteLine("\"\"," + val);
                }
            }
            return 0;
        }
        #endregion

        #region Insert Initial Fields
        private async Task<int> insertYear()
        {
            List<Year> listYear = new List<Year>();
            string[] years = { "2014", "2015", "2016", "2017", "2018", "2019"};
            for (int i = 0; i < years.Length; i++)
            {
                listYear.Add(new Year() { nameYear = years[i] });
            }
            await Task.Run(() =>
            {
                if (db.YearSet.Count() == 0)
                {
                    db.YearSet.AddRange(listYear);
                    db.SaveChanges();
                }
            });
            return 0;
        }

        private async Task<int> insertNewYear()
        {
            List<Year> listYear = new List<Year>();
            string[] years = {  "2019" };
            for (int i = 0; i < years.Length; i++)
            {
                listYear.Add(new Year() { nameYear = years[i] });
            }
            await Task.Run(() =>
            {
                using (var db = new Model1Container())
                {
                    //if (db.YearSet.Count() == 0)
                    //{
                    db.YearSet.AddRange(listYear);
                    db.SaveChanges();
                    //}
                }
            });
            return 0;
        }

        private async Task<int> insertSessions()
        {
            List<Session> listSession = new List<Session>();
            string[] sessions = { "Q1", "Q2", "Q3", "Q4" };
            //string[] sessions = { "2014Q1", "2014Q2", "2014Q3", "2014Q4"};
            for (int i = 0; i < sessions.Length; i++)
            {
                listSession.Add(new Session() { idSession = i + 1, nameSession = sessions[i] });
            }
            await Task.Run(() =>
            {
                if (db.SessionSet.Count() == 0)
                {
                    db.SessionSet.AddRange(listSession);
                    db.SaveChanges();
                }
            });
            return 0;
        }

        private async Task<int> insertMonths()
        {
            List<Month> listMonth = new List<Month>();
            string[] months = { "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12" };
            for (int i = 0; i < months.Length; i++)
            {
                listMonth.Add(new Month() { idMonth = i + 1, nameMonth = months[i] });
            }
            await Task.Run(() =>
            {
                if (db.MonthSet.Count() == 0)
                {
                    db.MonthSet.AddRange(listMonth);
                    db.SaveChanges();
                }
            });
            return 0;
        }

        private async Task<int> insertAssetsFieldsAndGroup()
        {
            int i = 1;
            int j = 1;
            string line;
            string[] splitStr;
            AssetsFields tmpField;
            await Task.Run(() =>
            {
                if (db.AssetsFieldsGroupSet.Count() == 0)
                {
                    using (var reader = new StreamReader(headDir + @"banalanceField.csv", System.Text.Encoding.GetEncoding("Big5")))
                    {
                        while (!reader.EndOfStream)
                        {
                            line = reader.ReadLine();
                            splitStr = line.Split(',');
                            if (splitStr[0].Length > 0)
                            {
                                db.AssetsFieldsGroupSet.Add(new AssetsFieldsGroup() { idAssetsFieldGroup = i, nameAssetsFieldGroup = splitStr[0] });
                                i++;
                            }
                            else
                            {
                                tmpField = new AssetsFields() { idAssetsField = j, nameAssetsField = splitStr[1] };
                                db.AssetsFieldsGroupSet.Find(i - 1).AssetsFields.Add(tmpField);
                                db.AssetsFieldsSet.Add(tmpField);
                                j++;
                            }
                        }
                        db.SaveChanges();
                    }
                }
            });
            return 0;
        }

        private async Task<int> insertPerformanceField()
        {
            List<PerformanceFields> listPerformanceFields = new List<PerformanceFields>();
            string[] fields = { "股本(億)", "收盤", "平均", "漲跌", "漲跌(%)", "營業收入(億)", "營業毛利(億)", "營業利益(億)", "業外損益(億)", "稅後淨利(億)", "營業毛利(%)", "營業利益(%)", "業外損益(%)", "稅後淨利(%)", "單季ROE(%)", "年估ROE(%)", "單季ROA(%)", "年估ROA(%)", "稅後EPS", "成長(元)", "BPS(元)" };
            for (int i = 0; i < fields.Length; i++)
            {
                listPerformanceFields.Add(new PerformanceFields() { idPerformanceField = i + 1, namePerformanceField = fields[i] });
            }
            await Task.Run(() =>
            {
                if (db.PerformanceFieldsSet.Count() == 0)
                {
                    db.PerformanceFieldsSet.AddRange(listPerformanceFields);
                    db.SaveChanges();
                }
            });
            return 0;
        }

        private async Task<int> insertSaleMonthField()
        {
            List<SaleMonthFields> listSaleMonthFields = new List<SaleMonthFields>();
            string[] fields = { "開盤", "收盤", "最高", "最低", "漲跌(元)", "漲跌(%)", "單月營收(億)", "月增(%)", "年增(%)", "累計營收(億)", "累計年增(%)", "合併單月營收(億)", "合併月增(%)", "合併年增(%)", "合併累計營收(億)", "合併累計年增(%)" };
            for (int i = 0; i < fields.Length; i++)
            {
                listSaleMonthFields.Add(new SaleMonthFields() { idSaleMonthField = i + 1, nameSaleMonthField = fields[i] });
            }
            await Task.Run(() =>
            {
                if (db.SaleMonthFieldsSet.Count() == 0)
                {
                    db.SaleMonthFieldsSet.AddRange(listSaleMonthFields);
                    db.SaveChanges();
                }
            });
            return 0;
        }

        private async Task<int> insertCashField()
        {
            List<CashFields> listCashFields = new List<CashFields>();
            string[] fields = { "平均股本(億)", "上期收盤", "本期收盤", "漲跌(元)", "漲跌(%)", "稅前淨利(億)", "稅後淨利(億)", "營業活動(億)", "投資活動(億)", "融資活動(億)", "其他活動(億)", "淨現金流(億)", "自由金流(億)", "期初餘額(億)", "期末餘額(億)", "現金流量(%)", "稅後EPS(元)" };
            for (int i = 0; i < fields.Length; i++)
            {
                listCashFields.Add(new CashFields() { idCashField = i + 1, nameCashField = fields[i] });
            }
            await Task.Run(() =>
            {
                if (db.CashFieldsSet.Count() == 0)
                {
                    db.CashFieldsSet.AddRange(listCashFields);
                    db.SaveChanges();
                }
            });
            return 0;
        }

        private async Task<int> insertDividendField()
        {
            List<DivendendFields> listDividendFields = new List<DivendendFields>();
            string[] fields = { "現金股利盈餘", "現金股利公積", "現金股利合計", "股票股利盈餘", "股票股利公積", "股票股利合計", "股利合計", "股利總計(億)", "股利總計(千張)", "董監酬勞(百萬)", "董監酬勞佔淨利(%)", "員工紅利(億)", "員工紅利股票(千張)", "最高", "最低", "年均", "現金殖利率(%)", "股票殖利率(%)", "合計殖利率(%)", "EPS(元)", "盈餘分配率(%)-配息", "盈餘分配率(%)-配股", "盈餘分配率(%)-合計" };
            for (int i = 0; i < fields.Length; i++)
            {
                listDividendFields.Add(new DivendendFields() { idDividendField = i + 1, nameDividendField = fields[i] });
            }
            await Task.Run(() =>
            {
                if (db.DivendendFieldsSet.Count() == 0)
                {
                    db.DivendendFieldsSet.AddRange(listDividendFields);
                    db.SaveChanges();
                }
            });
            return 0;
        }

        private async Task<int> insertAllFields()
        {
            int x = 0;
            //x = await insertStocks(headDir + @"stock.csv", true);
            //x = await insertStocks(headDir + @"stock1.csv", false);
            x = await insertYear();
            //x = await insertSessions();
            //x = await insertMonths();
            //x = await insertPerformanceField();
            //x = await insertSaleMonthField();
            //x = await insertCashField();
            //x = await insertDividendField();
            //x = await insertAssetsFieldsAndGroup();
            return x;
        }

        private async Task<int> insertStocks(string path, bool isCompany)
        {
            loopFileGetStockNumber(path);
            List<Stock> listStock = new List<Stock>();
            foreach (KeyValuePair<int, string> pair in dictStock)
            {
                listStock.Add(new Stock() { idStock = pair.Key, nameStock = pair.Value, isCompany = isCompany });
            }
            await Task.Run(() =>
            {
                if (db.StockSet.Where(s => s.isCompany == isCompany).Count() == 0)
                {
                    db.StockSet.AddRange(listStock);
                    db.SaveChanges();
                }

            });
            return 0;
        }
        #endregion

        #region Get Fields And Stocks
        private void getDBAssetsFields()
        {
            dictAssetsField = new Dictionary<string, int>();
            using (var db = new Model1Container())
            {
                List<AssetsFields> listAssets = db.AssetsFieldsSet.ToList<AssetsFields>();
                foreach (AssetsFields field in listAssets)
                {
                    if (dictAssetsField.ContainsKey(field.nameAssetsField))
                        Console.WriteLine(field.nameAssetsField);
                    dictAssetsField.Add(field.nameAssetsField, field.idAssetsField);
                }
            }
        }

        private void getDBSessions()
        {
            dictSession = new Dictionary<string, int>();
            using (var db = new Model1Container())
            {
                List<Session> listSessions = db.SessionSet.ToList<Session>();
                foreach (Session session in listSessions)
                {
                    dictSession.Add(session.nameSession, session.idSession);
                }
            }
        }

        private void getDBYears()
        {
            dictYear = new Dictionary<string, int>();
            using (var db = new Model1Container())
            {
                List<Year> listYear = db.YearSet.ToList<Year>();
                foreach (Year year in listYear)
                {
                    dictYear.Add(year.nameYear, year.idYear);
                }
            }
        }

        private void getDBMonths()
        {
            dictMonth = new Dictionary<string, int>();
            using (var db = new Model1Container())
            {
                List<Month> listMonth = db.MonthSet.ToList<Month>();
                foreach (Month month in listMonth)
                {
                    dictMonth.Add(month.nameMonth, month.idMonth);
                }
            }
        }

        private List<Stock> getDBStocks()
        {
            List<Stock> listStocks = new List<Stock>();
            dictStock = new Dictionary<int, string>();
            using (var db = new Model1Container())
            {
                listStocks = db.StockSet.ToList<Stock>();
                foreach (Stock stock in listStocks)
                {
                    dictStock.Add(stock.idStock, stock.nameStock);
                }
            }
            return listStocks;
        }

        private void testGetField()
        {
            List<string> listField = new List<string>();
            using (var reader = new StreamReader(@"V:\stock\field.csv", System.Text.Encoding.GetEncoding("Big5")))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    if (line != string.Empty)
                    {
                        if (!listField.Contains(line))
                            listField.Add(line);
                    }
                }
            }
        }
        #endregion

        #region Insert Stock Informantion
        private void insertStockBalance(Stock stock)
        {
            Console.WriteLine(DateTime.Now.ToString() + " " + stock.idStock + " Begin Balance");
            List<List<string>> table;
            string path;
            string htmlText;
            string htmlStr;
            HtmlDocument doc1;
            string fieldGroup = string.Empty;
            Assets tmpAsset;
            List<Assets> tmpListAsset;
            int tmpIdSession, tmpIdYear;
            foreach (string year in dictYear.Keys)
            {
                using (var db = new Model1Container())
                {
                    foreach (string season in dictSession.Keys)
                    {
                        tmpListAsset = new List<Assets>();
                        tmpIdSession = dictSession[season];
                        tmpIdYear = dictYear[year];
                        if (stock.isCompany)
                        {
                            path = headDir + @"\stockAsset\" + stock.idStock +
                                    @"\balance_" + year + season + ".html";
                        }
                        else
                        {
                            path = headDir + @"\stockAssetNoCompany\" + stock.idStock +
                              @"\" + stock.idStock + @"_asset_" + year + season + ".html";
                        }
                        if (!File.Exists(path))
                            continue;
                        htmlText = File.ReadAllText(path);
                        doc1 = new HtmlAgilityPack.HtmlDocument();
                        if (!htmlText.Contains("查無所需資料"))
                        {
                            doc1.LoadHtml(htmlText);
                            try
                            {
                                table = doc1.DocumentNode.SelectSingleNode("//table[@class='hasBorder']")
                               .Descendants("tr").Where(tr => tr.Elements("td").Count() > 1).Select(tr => tr.Elements("td").Select(td => td.InnerText.Trim()).ToList()).ToList();
                            }
                            catch (Exception)
                            {
                                table = null;
                            }
                            if (table == null)
                            {
                                Console.WriteLine(DateTime.Now.ToString() + " " + stock.idStock + " Null Balance " + year + season);
                                continue;
                            }
                            if (db.AssetsSet.Where(asset => asset.idStock == stock.idStock && asset.idYear == tmpIdYear && asset.idSession == tmpIdSession).FirstOrDefault() == null)
                            {
                                foreach (List<string> str in table)
                                {
                                    if (str[1].Length > 0)
                                    {
                                        htmlStr = WebUtility.HtmlDecode(str[0]).Trim();
                                        htmlStr = transferBalanceFieldGroup(htmlStr);
                                        htmlStr = transferBalanceField(htmlStr);
                                        if (dictAssetsField.ContainsKey(htmlStr))
                                        {
                                            tmpAsset = new Assets() { cost = str[1], percentage = str[2], idStock = stock.idStock, idAssetsField = dictAssetsField[htmlStr], idSession = tmpIdSession, idYear = tmpIdYear };
                                            tmpListAsset.Add(tmpAsset);
                                        }
                                        else
                                        {
                                            Console.WriteLine(year + season + " " + stock.idStock + " " + htmlStr);
                                        }
                                    }
                                }
                            }
                        }
                        db.AssetsSet.AddRange(tmpListAsset);
                        db.SaveChanges();
                    }

                }
            }

        }

        private void insertStockPerformance(Stock stock)
        {
            Console.WriteLine(DateTime.Now.ToString() + " " + stock.idStock + " Begin Performance");
            List<List<string>> table;
            string htmlText;
            HtmlDocument doc1;
            int a, idYear, idSeason;
            string year;
            string season;
            string filePath;
            string tmpStr;
            double tmpResult;
            List<Performance> listPerformance;
            Performance tmpPerformance;
            if (stock.isCompany)
            {
                filePath = headDir + @"\stockPerformance\" + stock.idStock +
                   @"\performance_" + stock.idStock + ".html";
                if (!File.Exists(filePath))
                {
                    Console.WriteLine(DateTime.Now.ToString() + " " + stock.idStock + " No performance html");
                    return;
                }
                htmlText = File.ReadAllText(headDir + @"\stockPerformance\" + stock.idStock +
                   @"\performance_" + stock.idStock + ".html");
            }
            else
            {
                filePath = headDir + @"\stockPerformanceNoCompany\" + stock.idStock +
                  @"\performance_" + stock.idStock + ".html";
                if (!File.Exists(filePath))
                {
                    Console.WriteLine(DateTime.Now.ToString() + " " + stock.idStock + " No performance html");
                    return;
                }
                htmlText = File.ReadAllText(headDir + @"\stockPerformanceNoCompany\" + stock.idStock +
                  @"\performance_" + stock.idStock + ".html");
            }

            doc1 = new HtmlAgilityPack.HtmlDocument();
            doc1.LoadHtml(htmlText);
            try
            {
                //table = doc1.DocumentNode.SelectNodes("//table[@class='solid_1_padding_3_0_tbl']").Last()
                //      .Descendants("tr").Where(tr => tr.Elements("td").Count() > 1).Select(tr => tr.Elements("td").Select(td => td.InnerText.Trim()).ToList()).ToList();
                table = doc1.DocumentNode.SelectNodes("//table[@class='solid_1_padding_4_0_tbl']").Last()
                      .Descendants("tr").Where(tr => tr.Elements("td").Count() > 1).Select(tr => tr.Elements("td").Select(td => td.InnerText.Trim()).ToList()).ToList();
            }
            catch (Exception)
            {
                table = null;
            }
            if (table == null)
            {
                Console.WriteLine(DateTime.Now.ToString() + " " + stock.idStock + " Null performance");
                return;
            }
            listPerformance = new List<Performance>();
            //tmplistPerformance = db.PerformanceSet.Where(stk => stk.idStock == stock.idStock).ToList();
            using (var db = new Model1Container())
            {
                foreach (List<string> str in table)
                {
                    if (str[0] != "季度" && str[0] != "收盤")
                    {
                        year = str[0].Substring(0, 4);
                        season = str[0].Substring(4);
                        if (year != "2020" && dictYear.ContainsKey(year))
                        {
                            idYear = dictYear[year];
                            idSeason = dictSession[season];
                            if (db.PerformanceSet.Where(per => per.idStock == stock.idStock && per.idSession == idSeason && per.idYear == idYear).FirstOrDefault() == null)
                            {
                                a = 1;
                                if (str[19] != "-")
                                {
                                    for (int i = 0; i < str.Count; i++)
                                    {
                                        if (i != 0 && i != 2)
                                        {
                                            tmpStr = str[i].Replace(",", "");
                                            if (double.TryParse(tmpStr, out tmpResult))
                                            {
                                                tmpStr = tmpResult.ToString();
                                            }
                                            tmpPerformance = new Performance() { idPeformanceField = a, idSession = idSeason, idYear = idYear, idStock = stock.idStock, valuePerformance = tmpStr };
                                            listPerformance.Add(tmpPerformance);
                                            a++;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                db.PerformanceSet.AddRange(listPerformance);
                db.SaveChanges();
            }
        }

        private void insertStockCash(Stock stock)
        {
            Console.WriteLine(DateTime.Now.ToString() + " " + stock.idStock + " Begin Cash");
            List<List<string>> table;
            string htmlText;
            HtmlDocument doc1;
            int a, idYear, idSeason;
            string year;
            string season;
            string tmpStr;
            double tmpResult;
            List<Cash> listCash;
            Cash tmpCash;
            if (stock.isCompany)
            {
                htmlText = File.ReadAllText(headDir + @"\stockCash\" + stock.idStock +
                        @"\cash_" + stock.idStock + ".html");
            }
            else
            {
                htmlText = File.ReadAllText(headDir + @"\stockCashNoCompany\" + stock.idStock +
                  @"\cash_" + stock.idStock + ".html");
            }
            doc1 = new HtmlAgilityPack.HtmlDocument();
            doc1.LoadHtml(htmlText);
            try
            {
                table = doc1.DocumentNode.SelectNodes("//table[@class='solid_1_padding_3_1_tbl']").Last()
                          .Descendants("tr").Where(tr => tr.Elements("td").Count() > 1).Select(tr => tr.Elements("td").Select(td => td.InnerText.Trim()).ToList()).ToList();
            }
            catch (Exception)
            {
                table = null;
            }
            if (table == null)
            {
                Console.WriteLine(DateTime.Now.ToString() + " " + stock.idStock + " Null Cash");
                return;
            }
            listCash = new List<Cash>();
            using (var db = new Model1Container())
            {
                foreach (List<string> str in table)
                {
                    if (str[0].Length > 0 && str[0] != "季度" && str[0] != "收盤")
                    {
                        year = str[0].Substring(0, 4);
                        season = str[0].Substring(4);
                        if (dictYear.ContainsKey(year))
                        {
                            idYear = dictYear[year];
                            idSeason = dictSession[season];
                            if (db.CashSet.Where(cash => cash.idStock == stock.idStock && cash.idYear == idYear && cash.idSession == idSeason).FirstOrDefault() == null)
                            {
                                a = 1;
                                for (int i = 0; i < str.Count; i++)
                                {
                                    if (i != 0 && i != 2)
                                    {
                                        tmpStr = str[i].Replace(",", "");
                                        if (double.TryParse(tmpStr, out tmpResult))
                                        {
                                            tmpStr = tmpResult.ToString();
                                        }
                                        tmpCash = new Cash() { idCashField = a, idSession = idSeason, idYear = idYear, idStock = stock.idStock, valueCash = tmpStr };
                                        listCash.Add(tmpCash);
                                        a++;
                                    }
                                }
                            }
                        }
                    }
                }
                db.CashSet.AddRange(listCash);
                db.SaveChanges();
            }
        }

        private void insertStockSaleMonth(Stock stock)
        {
            Console.WriteLine(DateTime.Now.ToString() + " " + stock.idStock + " Begin Sale Month");
            List<List<string>> table;
            string htmlText;
            HtmlDocument doc1;
            int a, idYear, idMonth;
            string year;
            string month;
            string tmpStr;
            double tmpResult;
            string filePath = string.Empty;
            List<SaleMonth> listSaleMonth;
            SaleMonth tmpSaleMonth;
            if (stock.isCompany)
            {
                filePath = headDir + @"\stockSaleMonth\" + stock.idStock +
                        @"\salemonth_" + stock.idStock + ".html";
            }
            else
            {
                filePath = headDir + @"\stockSaleMonthNoCompany\" + stock.idStock +
                  @"\salemonth_" + stock.idStock + ".html";               
            }
            if (File.Exists(filePath))
                htmlText = File.ReadAllText(filePath);
            else
            {
                Console.WriteLine(DateTime.Now.ToString() + " " + stock.idStock + " No Sale Month html");
                return;
            }
            doc1 = new HtmlAgilityPack.HtmlDocument();
            doc1.LoadHtml(htmlText);
            try
            {
                table = doc1.DocumentNode.SelectNodes("//table[@class='solid_1_padding_4_2_tbl']").Last()
                          .Descendants("tr").Where(tr => tr.Elements("td").Count() > 1).Select(tr => tr.Elements("td").Select(td => td.InnerText.Trim()).ToList()).ToList();
            }
            catch (Exception)
            {
                table = null;
            }
            if (table == null)
            {
                Console.WriteLine(DateTime.Now.ToString() + " " + stock.idStock + " Null Sale Month");
                return;
            }
            listSaleMonth = new List<SaleMonth>();
            using (var db = new Model1Container())
            {
                foreach (List<string> str in table)
                {
                    if (str[0] != "月別" && str[0] != "開盤")
                    {
                        year = str[0].Substring(0, 4);
                        month = str[0].Substring(5);
                        if (dictYear.ContainsKey(year))
                        {
                            idYear = dictYear[year];
                            idMonth = dictMonth[month];
                            if (db.SaleMonthSet.Where(sale => sale.idStock == stock.idStock && sale.idMonth == idMonth && sale.idYear == idYear).FirstOrDefault() == null)
                            {
                                a = 1;
                                for (int i = 0; i < str.Count; i++)
                                {
                                    if (i != 0)
                                    {
                                        tmpStr = str[i].Replace(",", "");
                                        if (double.TryParse(tmpStr, out tmpResult))
                                        {
                                            tmpStr = tmpResult.ToString();
                                        }
                                        tmpSaleMonth = new SaleMonth() { idSaleMonthField = a, idMonth = idMonth, idYear = idYear, idStock = stock.idStock, valueSaleMonth = tmpStr };
                                        listSaleMonth.Add(tmpSaleMonth);
                                        a++;
                                    }
                                }
                            }
                        }
                    }
                }
                db.SaleMonthSet.AddRange(listSaleMonth);
                db.SaveChanges();
            }
        }

        private void insertStockDividend(Stock stock)
        {
            Console.WriteLine(DateTime.Now.ToString() + " " + stock.idStock + " Begin Dividend");
            List<List<string>> table;
            string htmlText;
            HtmlDocument doc1;
            int a, idYear;
            string year;
            string tmpStr;
            double tmpResult;
            string filePath = string.Empty;
            List<Dividend> listDividend;
            Dividend tmpDividend;
            if (stock.isCompany)
            {
                filePath = headDir + @"\stockDividend\" + stock.idStock +
                        @"\dividend_" + stock.idStock + ".html";
                //htmlText = File.ReadAllText(headDir + @"\stockDividend\" + stock.idStock +
                //        @"\dividend_" + stock.idStock + ".html");
            }
            else
            {
                filePath = headDir + @"\stockDividendNoCompany\" + stock.idStock +
                  @"\dividend_" + stock.idStock + ".html";
                //htmlText = File.ReadAllText(headDir + @"\stockDividendNoCompany\" + stock.idStock +
                //  @"\dividend_" + stock.idStock + ".html");
            }
            if (File.Exists(filePath))
                htmlText = File.ReadAllText(filePath);
            else
            {
                Console.WriteLine(DateTime.Now.ToString() + " " + stock.idStock + " No Dividend html");
                return;
            }
            doc1 = new HtmlAgilityPack.HtmlDocument();
            doc1.LoadHtml(htmlText);
            try
            {
                table = doc1.DocumentNode.SelectNodes("//table[@class='solid_1_padding_4_0_tbl']").Last()
                          .Descendants("tr").Where(tr => tr.Elements("td").Count() > 1).Select(tr => tr.Elements("td").Select(td => td.InnerText.Trim()).ToList()).ToList();
            }
            catch (Exception)
            {
                table = null;
            }
            if (table == null)
            {
                Console.WriteLine(DateTime.Now.ToString() + " " + stock.idStock + " Null Dividend");
                return;
            }
            listDividend = new List<Dividend>();
            using (var db = new Model1Container())
            {
                foreach (List<string> str in table)
                {
                    if (str[0] != "股　　利　　政　　策" && str[0] != "股利發放年度" && str[0] != "現金股利" && str[0] != "盈餘" && str[0] != "-" && str[0] != "平均" && str[0] != "累計")
                    {
                        year = str[0].Substring(0, 4);
                        if (dictYear.ContainsKey(year))
                        {
                            idYear = dictYear[year];
                            if (db.DividendSet.Where(dividend => dividend.idStock == stock.idStock && dividend.idYear == idYear).FirstOrDefault() == null)
                            {
                                a = 1;
                                if (str[20] != "-")
                                {
                                    for (int i = 0; i < str.Count; i++)
                                    {
                                        if (i != 0 && i != 14 && i != 21)
                                        {
                                            tmpStr = str[i].Replace(",", "");
                                            if (double.TryParse(tmpStr, out tmpResult))
                                            {
                                                tmpStr = tmpResult.ToString();
                                            }
                                            tmpDividend = new Dividend() { idDividendField = a, idYear = idYear, idStock = stock.idStock, valueDividend = tmpStr };
                                            listDividend.Add(tmpDividend);
                                            a++;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                if (listDividend.Count > 0)
                {
                    db.DividendSet.AddRange(listDividend);
                    db.SaveChanges();
                }
            }
        }
        #endregion

        private void getDBStockBlance(int stockNum)
        {
            using (var db = new Model1Container())
            {
                var leftOuterJoin =
                    from first in db.AssetsFieldsSet
                    join last in db.AssetsSet.Where(o => o.idSession == 1)
                    on first.idAssetsField equals last.idAssetsField
                    into temp
                    from last in temp.DefaultIfEmpty()
                    select new
                    {
                        first.idAssetsField,
                        first.nameAssetsField,
                        last.cost,
                        last.percentage
                    };
                foreach (var item in leftOuterJoin)
                {
                    Console.WriteLine(item.idAssetsField + item.nameAssetsField + item.cost);
                }
            }
        }

        #region Select Stock Rules
        private void selectStockRule1()
        {
            int idMaxYear = db.YearSet.Last().idYear;
            var getStock = from sale in db.SaleMonthSet.Where(a => a.idSaleMonthField == 16)
                           join year in db.YearSet.Where(t => t.idYear == idMaxYear)
                           on sale.idYear equals year.idYear
                           join month in db.MonthSet.Where(b => b.idMonth == 12)
                           on sale.idMonth equals month.idMonth
                           select new
                           {
                               sale
                           };
            double result;
            foreach (var stock in getStock)
            {
                if (double.TryParse(stock.sale.valueSaleMonth, out result))
                {
                    if (result > 10)
                    {
                        Console.WriteLine(stock.sale.Stock.nameStock);
                    }
                }
            }
        }

        private List<int> getAllStockID()
        {

            List<int> getStock = new List<int>();
            using (var db = new Model1Container())
            {                
                getStock = db.StockSet.Select(stk => stk.idStock).ToList();
            }
            return getStock;
        }

        private List<int> selectStockRule2()
        {
            Year year;
            int idMaxYear;
            List<int> getStock = getAllStockID();
            List<int> listRuleStock = new List<int>();
            double result;
            SaleMonth tmpSaleMonth;
            int dividendCount = 0;
            double dividendPercentage;     
            using(var db = new Model1Container())
            {
                year = db.YearSet.ToList().LastOrDefault();
                idMaxYear = year.idYear;
            }       
            foreach (int stock in getStock)
            {                
                using (var db = new Model1Container())
                {
                    Console.WriteLine(DateTime.Now.ToString() + " " + stock + " Begin Rule2");
                    tmpSaleMonth = db.SaleMonthSet.Where(sale => sale.idStock == stock && sale.idYear == idMaxYear && sale.idMonth == 1 && sale.idSaleMonthField == 16).SingleOrDefault();
                    if (tmpSaleMonth == null)
                        continue;
                    if (double.TryParse(tmpSaleMonth.valueSaleMonth, out result))
                    {
                        if (result > 10)
                        {
                            tmpSaleMonth = db.SaleMonthSet.Where(sale => sale.idStock == stock && sale.idYear == idMaxYear - 1 && sale.idMonth == 1 && sale.idSaleMonthField == 16).SingleOrDefault();
                            if (tmpSaleMonth == null)
                                continue;
                            double.TryParse(tmpSaleMonth.valueSaleMonth, out result);
                            if (result > 5)
                            {
                                var tmpList = db.DividendSet.Where(dividend =>dividend.idStock == stock && dividend.idDividendField == 19).ToList().Take(3).Reverse();
                                dividendPercentage = 0;
                                dividendCount = 0;
                                foreach (Dividend tmp in tmpList)
                                {
                                    dividendCount++;
                                    double.TryParse(tmp.valueDividend, out result);
                                    dividendPercentage += result;
                                }
                                dividendPercentage = dividendPercentage / dividendCount;
                                if (dividendPercentage > 3)
                                {
                                    Console.WriteLine(stock);
                                    listRuleStock.Add(stock);
                                }
                            }
                        }
                    }
                }
            }
            return listRuleStock;
        }        

        private List<int> selectStockRule3(int stockValueMonth)
        {
            List<int> getStock = getAllStockID();
            List<int> selectStock = new List<int>();
            double result;
            double fourSeasonEPS = 0;
            double fourSeasonEPS1 = 0;
            double fourSeasonEPS2 = 0;
            double increaseEPS = 0;
            double increaseEPS1 = 0;
            bool opm;
            int i = 0;
            Year year;
            int idMaxYear;
            double lastDividend = 0;
            double averageDividend = 0;
            double lowestValue = 0;
            double highestValue = 0;
            double stockValue = 0;
            double averageValue = 0;
            string writeStr = string.Empty;
            using (var db = new Model1Container())
            {
                year = db.YearSet.ToList().LastOrDefault();
                idMaxYear = year.idYear;
            }
            //getStock.Clear();
            //getStock.Add(5439);
            foreach (int stock in getStock)
            {
                using (var db = new Model1Container())
                {
                    Console.WriteLine(DateTime.Now.ToString() + " " + stock + " Begin Rule2");
                    var tmpPerformance1 = db.PerformanceSet.Where(item => item.idStock == stock && item.idPeformanceField == 12).ToList().Take(12);
                    if (tmpPerformance1.Count() != 12)
                        continue;
                    opm = true;
                    foreach (Performance perItem in tmpPerformance1)
                    {
                        double.TryParse(perItem.valuePerformance, out result);
                        if (result < 0)
                        {
                            opm = false;
                            break;
                        }
                    }
                    if (!opm)
                        continue;
                    var tmpPerformance = db.PerformanceSet.Where(item => item.idStock == stock && item.idPeformanceField == 19).ToList().OrderByDescending(item => item.idSession).OrderByDescending(item => item.idYear).Take(12);                    
                    i = 0;
                    fourSeasonEPS = 0;
                    fourSeasonEPS1 = 0;
                    fourSeasonEPS2 = 0;
                    foreach (Performance perItem in tmpPerformance)
                    {
                        if (i < 4)
                        {
                            double.TryParse(perItem.valuePerformance, out result);
                            fourSeasonEPS += result;
                        }
                        else if (i >= 4 &&  i < 8)
                        {
                            double.TryParse(perItem.valuePerformance, out result);
                            fourSeasonEPS1 += result;
                        }
                        else
                        {
                            double.TryParse(perItem.valuePerformance, out result);
                            fourSeasonEPS2 += result;
                        }
                        i++;
                    }
                    increaseEPS = fourSeasonEPS - fourSeasonEPS1;
                    increaseEPS1 = fourSeasonEPS1 - fourSeasonEPS2;
                    if(increaseEPS > 0 && increaseEPS1 > 0)
                    {
                        increaseEPS = increaseEPS / fourSeasonEPS * 100;
                        increaseEPS1 = increaseEPS1 / fourSeasonEPS1 * 100;
                        //increaseEPS = (increaseEPS + increaseEPS1) / 2;
                        if (increaseEPS > 10 && increaseEPS1 > 10)
                        {
                            var tmpDividend = db.DividendSet.Where(item => item.idStock == stock && item.idDividendField == 19 && item.valueDividend != "-").ToList().OrderByDescending(item => item.idYear).Take(3);
                            if (tmpDividend.Count() != 3)
                                continue;
                            i = 0;
                            averageDividend = 0;
                            foreach (Dividend item in tmpDividend)
                            {
                                double.TryParse(item.valueDividend, out result);
                                if (i == 0)
                                    lastDividend = result;
                                averageDividend += result;
                                i++;
                            }
                            averageDividend = averageDividend / 3;
                            var tmpStockValue = db.SaleMonthSet.Where(item => item.idStock == stock && item.idYear == idMaxYear && item.idMonth == stockValueMonth &&(item.idSaleMonthField == 3 || item.idSaleMonthField == 4));
                            foreach(SaleMonth item in tmpStockValue)
                            {
                                switch(item.idSaleMonthField)
                                {
                                    case 3:
                                        double.TryParse(item.valueSaleMonth, out highestValue);
                                        break;
                                    case 4:
                                        double.TryParse(item.valueSaleMonth, out lowestValue);
                                        break;
                                }

                            }
                            if (highestValue == 0 || lowestValue == 0)
                                continue;
                            stockValue = (highestValue + lowestValue) / 2;
                            averageValue = stockValue / fourSeasonEPS;
                            averageValue = (increaseEPS + lastDividend) / averageValue;
                            if (averageValue > 2)
                            {
                                using (System.IO.StreamWriter file =
                   new System.IO.StreamWriter(headDir + @"rules.csv", true, System.Text.Encoding.GetEncoding("Big5")))
                                {
                                    writeStr = stock + ",\"" + averageValue + "\",\"" + stockValue + "\",\"" + fourSeasonEPS + "\""; 
                                    file.WriteLine(writeStr);
                                }
                                selectStock.Add(stock);
                                Console.WriteLine(stock + " Increase EPS Percentage : " + increaseEPS);
                            }
                        }
                    }
                        
                }
            }
            return selectStock;
        }

        private List<int> selectStockRule3_1(int stockValueMonth)
        {
            List<int> getStock = getAllStockID();
            List<int> selectStock = new List<int>();
            double result;
            double fourSeasonEPS = 0;
            double fourSeasonEPS1 = 0;
            double fourSeasonEPS2 = 0;
            double increaseEPS = 0;
            double increaseEPS1 = 0;
            bool opm;
            int i = 0;
            Year year;
            int idMaxYear;
            double lastDividend = 0;
            double averageDividend = 0;            
            double stockValue = 0;
            double averageValue = 0;
            string writeStr = string.Empty;
            string nowStockPrice;
            using (var db = new Model1Container())
            {
                year = db.YearSet.ToList().LastOrDefault();
                idMaxYear = year.idYear;
            }            
            foreach (int stock in getStock)
            {
                using (var db = new Model1Container())
                {
                    Console.WriteLine(DateTime.Now.ToString() + " " + stock + " Begin Rule2");
                    var tmpPerformance1 = db.PerformanceSet.Where(item => item.idStock == stock && item.idPeformanceField == 12).ToList().Take(12);
                    if (tmpPerformance1.Count() != 12)
                        continue;
                    opm = true;
                    foreach (Performance perItem in tmpPerformance1)
                    {
                        double.TryParse(perItem.valuePerformance, out result);
                        if (result < 0)
                        {
                            opm = false;
                            break;
                        }
                    }
                    if (!opm)
                        continue;
                    var tmpPerformance = db.PerformanceSet.Where(item => item.idStock == stock && item.idPeformanceField == 19).ToList().OrderByDescending(item => item.idSession).OrderByDescending(item => item.idYear).Take(12);
                    i = 0;
                    fourSeasonEPS = 0;
                    fourSeasonEPS1 = 0;
                    fourSeasonEPS2 = 0;
                    foreach (Performance perItem in tmpPerformance)
                    {
                        if (i < 4)
                        {
                            double.TryParse(perItem.valuePerformance, out result);
                            fourSeasonEPS += result;
                        }
                        else if (i >= 4 && i < 8)
                        {
                            double.TryParse(perItem.valuePerformance, out result);
                            fourSeasonEPS1 += result;
                        }
                        else
                        {
                            double.TryParse(perItem.valuePerformance, out result);
                            fourSeasonEPS2 += result;
                        }
                        i++;
                    }
                    increaseEPS = fourSeasonEPS - fourSeasonEPS1;
                    increaseEPS1 = fourSeasonEPS1 - fourSeasonEPS2;
                    //if (increaseEPS > 0 && increaseEPS1 > 0)
                    if (increaseEPS > 0)
                    {
                        increaseEPS = increaseEPS / fourSeasonEPS * 100;
                        increaseEPS1 = increaseEPS1 / fourSeasonEPS1 * 100;
                        //increaseEPS = (increaseEPS + increaseEPS1) / 2;
                        //if (increaseEPS > 10 && increaseEPS1 > 10)
                        if (increaseEPS > 10)
                        {
                            var tmpDividend = db.DividendSet.Where(item => item.idStock == stock && item.idDividendField == 19 && item.valueDividend != "-").ToList().OrderByDescending(item => item.idYear).Take(3);
                            if (tmpDividend.Count() != 3)
                                continue;
                            i = 0;
                            averageDividend = 0;
                            foreach (Dividend item in tmpDividend)
                            {
                                double.TryParse(item.valueDividend, out result);
                                if (i == 0)
                                    lastDividend = result;
                                averageDividend += result;
                                i++;
                            }
                            averageDividend = averageDividend / 3;
                            if(db.StockSet.Where(item=>item.idStock == stock).First().isCompany)
                                nowStockPrice = DownloadByTwse(stock.ToString(), "20191120");
                            else
                                nowStockPrice = DownloadByTwse1(stock.ToString(), "108/11");
                            if (nowStockPrice == string.Empty)
                            {
                                Console.WriteLine(stock + "No Stock Price");
                                continue;
                            }
                            double.TryParse(nowStockPrice, out stockValue);                                                      
                            averageValue = stockValue / fourSeasonEPS;
                            averageValue = (increaseEPS + lastDividend) / averageValue;
                            if (averageValue > 1.8)
                            {
                                using (System.IO.StreamWriter file =
                   new System.IO.StreamWriter(saveDir + @"rules.csv", true, System.Text.Encoding.GetEncoding("Big5")))
                                {
                                    writeStr = stock + ",\"" + averageValue + "\",\"" + stockValue + "\",\"" + fourSeasonEPS + "\"";
                                    file.WriteLine(writeStr);
                                    var tmpSaleMonths = db.SaleMonthSet.Where(item => item.idStock == stock && item.idYear == idMaxYear && item.idMonth > 0 && (item.idSaleMonthField == 14 || item.idSaleMonthField == 16)).ToList().OrderByDescending(item => item.idMonth);
                                    foreach (SaleMonth item in tmpSaleMonths)
                                    {
                                        switch (item.idSaleMonthField)
                                        {
                                            case 14:
                                                writeStr = year.nameYear + "_" + item.idMonth + ",\"" + item.valueSaleMonth + "\"";
                                                break;
                                            case 16:
                                                writeStr += ",\"" + item.valueSaleMonth + "\"";
                                                file.WriteLine(writeStr);
                                                break;
                                        }
                                    }
                                }
                                selectStock.Add(stock);
                                Console.WriteLine(stock + " Increase EPS Percentage : " + increaseEPS);
                            }
                        }
                    }

                }
            }
            return selectStock;
        }

        private List<int> selectStockRule3MyStock(int stockValueMonth, List<int> getStock)
        {            
            List<int> selectStock = new List<int>();
            double result;
            double fourSeasonEPS = 0;
            double fourSeasonEPS1 = 0;
            double fourSeasonEPS2 = 0;
            double increaseEPS = 0;
            double increaseEPS1 = 0;
            bool opm;
            int i = 0;
            Year year;
            int idMaxYear;
            double lastDividend = 0;
            double averageDividend = 0;
            double stockValue = 0;
            double averageValue = 0;
            string writeStr = string.Empty;
            string nowStockPrice;
            using (var db = new Model1Container())
            {
                year = db.YearSet.ToList().LastOrDefault();
                idMaxYear = year.idYear;
            }
            foreach (int stock in getStock)
            {
                using (var db = new Model1Container())
                {
                    Console.WriteLine(DateTime.Now.ToString() + " " + stock + " Begin Rule2");
                    var tmpPerformance1 = db.PerformanceSet.Where(item => item.idStock == stock && item.idPeformanceField == 12).ToList().Take(12);
                    if (tmpPerformance1.Count() != 12)
                        continue;
                    opm = true;
                    foreach (Performance perItem in tmpPerformance1)
                    {
                        double.TryParse(perItem.valuePerformance, out result);
                        if (result < 0)
                        {
                            opm = false;
                            break;
                        }
                    }
                    if (!opm)
                        continue;
                    var tmpPerformance = db.PerformanceSet.Where(item => item.idStock == stock && item.idPeformanceField == 19).ToList().OrderByDescending(item => item.idSession).OrderByDescending(item => item.idYear).Take(12);
                    i = 0;
                    fourSeasonEPS = 0;
                    fourSeasonEPS1 = 0;
                    fourSeasonEPS2 = 0;
                    foreach (Performance perItem in tmpPerformance)
                    {
                        if (i < 4)
                        {
                            double.TryParse(perItem.valuePerformance, out result);
                            fourSeasonEPS += result;
                        }
                        else if (i >= 4 && i < 8)
                        {
                            double.TryParse(perItem.valuePerformance, out result);
                            fourSeasonEPS1 += result;
                        }
                        else
                        {
                            double.TryParse(perItem.valuePerformance, out result);
                            fourSeasonEPS2 += result;
                        }
                        i++;
                    }
                    increaseEPS = fourSeasonEPS - fourSeasonEPS1;
                    increaseEPS1 = fourSeasonEPS1 - fourSeasonEPS2;
                    //if (increaseEPS > 0 && increaseEPS1 > 0)
                    if (increaseEPS > 0)
                    {
                        increaseEPS = increaseEPS / fourSeasonEPS * 100;
                        increaseEPS1 = increaseEPS1 / fourSeasonEPS1 * 100;
                        //increaseEPS = (increaseEPS + increaseEPS1) / 2;
                        //if (increaseEPS > 10 && increaseEPS1 > 10)
                        if (increaseEPS > 10)
                        {
                            var tmpDividend = db.DividendSet.Where(item => item.idStock == stock && item.idDividendField == 19 && item.valueDividend != "-").ToList().OrderByDescending(item => item.idYear).Take(3);
                            if (tmpDividend.Count() != 3)
                                continue;
                            i = 0;
                            averageDividend = 0;
                            foreach (Dividend item in tmpDividend)
                            {
                                double.TryParse(item.valueDividend, out result);
                                if (i == 0)
                                    lastDividend = result;
                                averageDividend += result;
                                i++;
                            }
                            averageDividend = averageDividend / 3;
                            if (db.StockSet.Where(item => item.idStock == stock).First().isCompany)
                                nowStockPrice = DownloadByTwse(stock.ToString(), DateTime.Now.ToString("yyyyMMdd"));
                            else
                                nowStockPrice = DownloadByTwse1(stock.ToString(), "108/12");
                            if (nowStockPrice == string.Empty)
                                continue;
                            double.TryParse(nowStockPrice, out stockValue);
                            averageValue = stockValue / fourSeasonEPS;
                            averageValue = (increaseEPS + lastDividend) / averageValue;
                            //if (averageValue > 1.5)
                            //{
                                

                                using (System.IO.StreamWriter file =
                   new System.IO.StreamWriter(headDir + DateTime.Now.ToString("yyyy-MM-dd") + @"_MyStock.csv", true, System.Text.Encoding.GetEncoding("Big5")))
                                {
                                    writeStr = stock + ",\"" + averageValue + "\",\"" + stockValue + "\",\"" + fourSeasonEPS + "\"";
                                    file.WriteLine(writeStr);
                                    var tmpSaleMonths = db.SaleMonthSet.Where(item => item.idStock == stock && item.idYear == idMaxYear && item.idMonth > 0 && (item.idSaleMonthField == 14 || item.idSaleMonthField == 16)).ToList().OrderByDescending(item=>item.idMonth);
                                    foreach (SaleMonth item in tmpSaleMonths)
                                    {
                                        switch (item.idSaleMonthField)
                                        {
                                            case 14:
                                                writeStr = year.nameYear + "_" + item.idMonth + ",\"" + item.valueSaleMonth + "\"";
                                                break;
                                            case 16:
                                                writeStr += ",\"" + item.valueSaleMonth + "\"";
                                                file.WriteLine(writeStr);
                                                break;
                                        }
                                    }
                                }
                                selectStock.Add(stock);
                                Console.WriteLine(stock + " Increase EPS Percentage : " + increaseEPS);
                            //}
                        }
                    }

                }
            }
            return selectStock;
        }
        #endregion

        #region Get Now Stock Value
        public string DownloadByTwse1(string STOCK_CODE, string date)
        {
            Console.WriteLine("更新股價：" + STOCK_CODE);
            string close_price = string.Empty;
            string download_url = "http://www.tpex.org.tw/web/stock/aftertrading/daily_trading_info/st43_result.php?l=zh-tw&d=" + date + "&stkno=" + STOCK_CODE;
            // 系統睡10秒, 避免快速呼叫而被證交所擋ip
            System.Threading.Thread.Sleep(10000);
            string downloadedData = "";
            string closePrice = string.Empty;
            using (WebClient wClient = new WebClient())
            {
                try
                {
                    downloadedData = wClient.DownloadString(download_url);
                }
                catch (WebException ex)
                {
                    Console.WriteLine("更新股價失敗：" + STOCK_CODE + " " + ex.Message);
                }
            }
            if (downloadedData.Trim().Length == 0)
            {
                return string.Empty;
            }
            JObject o = JObject.Parse(downloadedData);
            JArray allPrices = (JArray)o["aaData"];
            for(int i  = allPrices.Count - 1; i > -1; i--)
            {
                string date1 = (string)allPrices[i][0]; //日期
                string open_price = (string)allPrices[i][3];//開盤價
                string high_price = (string)allPrices[i][4]; //最高價
                string low_price = (string)allPrices[i][5]; //最低價
                closePrice = (string)allPrices[i][6]; //收盤價    
                if (closePrice == "--")
                    continue;
                break;            
            }
            return closePrice;
        }

        public string DownloadByTwse(string STOCK_CODE, string date)
        {
            Console.WriteLine("更新股價：" + STOCK_CODE);
            string closePrice = string.Empty;
            //date = "20190805";
            string download_url = "http://www.twse.com.tw/exchangeReport/STOCK_DAY?response=json&date=" + date + "&stockNo=" + STOCK_CODE;

            // 系統睡10秒, 避免快速呼叫而被證交所擋ip
            System.Threading.Thread.Sleep(10000);
            string downloadedData = "";
            using (WebClient wClient = new WebClient())
            {
                ServicePointManager.SecurityProtocol =
                    SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls |
                    SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                wClient.Encoding = Encoding.UTF8; // 設定Webclient.Encoding
                try
                {
                    downloadedData = wClient.DownloadString(download_url);
                }
                catch (WebException ex)
                {
                    Console.WriteLine("更新股價失敗：" + STOCK_CODE + " " + ex.Message);
                }
            }
            if (downloadedData.Trim().Length == 0)
            {
                return string.Empty;
            }
            JObject o = JObject.Parse(downloadedData);
            JArray allPrices = (JArray)o["data"];
            if (allPrices == null)
            {
                System.Threading.Thread.Sleep(20000);
                Console.WriteLine("Sleep 20 for reget stock price");
                downloadedData = "";
                using (WebClient wClient = new WebClient())
                {
                    ServicePointManager.SecurityProtocol =
                        SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls |
                        SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                    wClient.Encoding = Encoding.UTF8; // 設定Webclient.Encoding
                    try
                    {
                        downloadedData = wClient.DownloadString(download_url);
                    }
                    catch (WebException ex)
                    {
                        Console.WriteLine("更新股價失敗：" + STOCK_CODE + " " + ex.Message);
                    }
                }
                if (downloadedData.Trim().Length == 0)
                {
                    return string.Empty;
                }
            }
            o = JObject.Parse(downloadedData);
            allPrices = (JArray)o["data"];
            for (int i = allPrices.Count - 1; i > -1; i--)
            {
                string date1 = (string)allPrices[i][0]; //日期
                string open_price = (string)allPrices[i][3];//開盤價
                string high_price = (string)allPrices[i][4]; //最高價
                string low_price = (string)allPrices[i][5]; //最低價
                closePrice = (string)allPrices[i][6]; //收盤價    
                break;
            }
            return closePrice;
        }

        public async Task<int> TestGetEPS(string STOCK_CODE)
        {
            string closePrice = string.Empty;
            string download_url = "http://mops.twse.com.tw/mops/web/ajax_t163sb15?encodeURIComponent=1&step=1&firstin=1&off=1&keyword4=&code1=&TYPEK2=&checkbtn=&queryName=co_id&t05st29_c_ifrs=N&t05st30_c_ifrs=N&inpuType=co_id&TYPEK=all&isnew=false&co_id=1101&year=107";
            List<List<string>> table;
            HtmlDocument doc1;
            float eps;
            // 系統睡10秒, 避免快速呼叫而被證交所擋ip
            //System.Threading.Thread.Sleep(10000);
            using (HttpClientHandler handler = new HttpClientHandler())
            {                
                using (HttpClient wClient = new HttpClient(handler))
                {
                    var formData = new FormUrlEncodedContent(new[] {
                    new KeyValuePair<string, string>("step", "1"),
                    new KeyValuePair<string, string>("CO_ID", "1101"),
                    new KeyValuePair<string, string>("SYEAR", "2018"),
                    new KeyValuePair<string, string>("SSEASON", "4"),
                    new KeyValuePair<string, string>("REPORT_ID", "B"),
                });
                    //wClient.PostAsync = Encoding.UTF8; // 設定Webclient.Encoding                
                    try
                    {
                        HttpResponseMessage response = null;
                        response = await wClient.PostAsync(download_url,null);
                        
                        if (response != null)
                        {
                            if (response.IsSuccessStatusCode == true)
                            {
                                //response.Content.Headers.ContentType.CharSet = "BIG5";
                                // 取得呼叫完成 API 後的回報內容
                                string strresult = await response.Content.ReadAsStringAsync();
                                doc1 = new HtmlDocument();
                                doc1.LoadHtml(strresult);
                                table = doc1.DocumentNode.SelectNodes("//table").Last()
                      .Descendants("tr").Where(tr => tr.Elements("td").Count() > 1 && tr.Elements("th").First().InnerText == "基本每股盈餘（元）").Select(tr => tr.Elements("td").Select(td => td.InnerText.Trim()).ToList()).ToList();
                                //var byteArray = await response.Content.ReadAsByteArrayAsync();                                
                                //var responseString = Encoding.Big.GetString(byteArray, 0, byteArray.Length);
                                //Where(tr => tr.Elements("th").First().InnerText.Trim() == "基本每股盈餘（元)")
                                if(table.Count() == 1 && table[0].Count == 4)
                                {
                                    foreach(var i in table[0])
                                    {
                                        if(float.TryParse(i,out eps))
                                            Console.WriteLine(i.ToString());
                                    }
                                }
                            }
                            else
                            {
                                Console.WriteLine("更新股價失敗：" + STOCK_CODE + " Fail");
                            }
                        }
                        else
                        {
                            Console.WriteLine("更新股價失敗：" + STOCK_CODE + " NULL");
                        }
                    }
                    catch (WebException ex)
                    {
                        Console.WriteLine("更新股價失敗：" + STOCK_CODE + " " + ex.Message);
                    }
                }
            }
            return 0;
        }
        #endregion

        private async void button_Click(object sender, RoutedEventArgs e)
        {
            int x;
            List<Stock> listStocks;
            List<int> rule2Stocks;
            List<int> rule3Stocks;
            List<int> multiRuleStocks = new List<int>();
            //if(db.SessionSet.Count() == 0)
            //    x = await insertSessions();
            //getDBSessions();
            //initialAssetsFieldsGroup();
            //x = await processBalanceHtml();            
            //x = await insertAllFields();
            //x = await insertStocks();
            //x = await insertNewYear();

            //x = await TestGetEPS("1101");

            await Task.Run(() =>
            {
                getDBSessions();
                getDBYears();
                getDBMonths();

                getDBAssetsFields();
                //    getDBSessions();
                listStocks = getDBStocks();
                foreach (Stock stock in listStocks)
                {
                    insertStockPerformance(stock);
                    insertStockSaleMonth(stock);
                    //insertStockCash(stock);
                    //insertStockDividend(stock);
                    //insertStockBalance(stock);
                }
                //rule2Stocks = selectStockRule2();

                //rule3Stocks = selectStockRule3_1(6);
                //    //List<int> myStocks = new List<int>();
                //    ////myStocks.Add(5289);
                //    ////myStocks.Add(3010);
                //    ////myStocks.Add(4528);
                //    ////myStocks.Add(5285);
                //    ////myStocks.Add(8155);
                //    ////myStocks.Add(1560);
                //    ////myStocks.Add(6138);
                //    ////myStocks.Add(1301);
                //    ////myStocks.Add(3388);
                //    ////myStocks.Add(2376);
                //    ////myStocks.Add(1788);
                //    ////myStocks.Add(6208);
                //    ////myStocks.Add(3217);
                //    //myStocks.Add(2441);
                //    //selectStockRule3MyStock(3,myStocks);
                //    //foreach (int stock in rule3Stocks)
                //    //{
                //    //    if (rule2Stocks.Contains(stock))
                //    //        multiRuleStocks.Add(stock);
                //    //}

                //    //DownloadByTwse1("3217", "107/03");

            });
        }

        private async void Button_run_Click(object sender, RoutedEventArgs e)
        {
            int x;
            List<Stock> listStocks;
            List<int> rule2Stocks;
            List<int> rule3Stocks;
            List<int> multiRuleStocks = new List<int>();
            //if(db.SessionSet.Count() == 0)
            //    x = await insertSessions();
            //getDBSessions();
            //initialAssetsFieldsGroup();
            //x = await processBalanceHtml();            
            //x = await insertAllFields();
            //x = await insertStocks();
            //x = await insertNewYear();

            //x = await TestGetEPS("1101");

            await Task.Run(() =>
            {
                getDBSessions();
                getDBYears();
                getDBMonths();

                getDBAssetsFields();
                //    getDBSessions();
                listStocks = getDBStocks();
                //foreach (Stock stock in listStocks)
                //{
                //insertStockPerformance(stock);
                //insertStockSaleMonth(stock);
                //insertStockCash(stock);
                //insertStockDividend(stock);
                //insertStockBalance(stock);
                //}
                //rule2Stocks = selectStockRule2();

                rule3Stocks = selectStockRule3_1(11);
                //    //List<int> myStocks = new List<int>();
                //    ////myStocks.Add(5289);
                //    ////myStocks.Add(3010);
                //    ////myStocks.Add(4528);
                //    ////myStocks.Add(5285);
                //    ////myStocks.Add(8155);
                //    ////myStocks.Add(1560);
                //    ////myStocks.Add(6138);
                //    ////myStocks.Add(1301);
                //    ////myStocks.Add(3388);
                //    ////myStocks.Add(2376);
                //    ////myStocks.Add(1788);
                //    ////myStocks.Add(6208);
                //    ////myStocks.Add(3217);
                //    //myStocks.Add(2441);
                //    //selectStockRule3MyStock(3,myStocks);
                //    //foreach (int stock in rule3Stocks)
                //    //{
                //    //    if (rule2Stocks.Contains(stock))
                //    //        multiRuleStocks.Add(stock);
                //    //}

                //    //DownloadByTwse1("3217", "107/03");

            });
        }

        private async void Button_run_custom_Click(object sender, RoutedEventArgs e)
        {            
            List<int> multiRuleStocks = new List<int>();
            //if(db.SessionSet.Count() == 0)
            //    x = await insertSessions();
            //getDBSessions();
            //initialAssetsFieldsGroup();
            //x = await processBalanceHtml();            
            //x = await insertAllFields();
            //x = await insertStocks();
            //x = await insertNewYear();
            List<int> myStocks = new List<int>();
            //x = await TestGetEPS("1101");
           
            if (txtbox_custom.Text.Length > 0)
            {                
                foreach (string stock in txtbox_custom.Text.Split(','))
                {
                    myStocks.Add(int.Parse(stock));
                }
                File.Delete("cutomStock.txt");
                File.WriteAllText("customStock.txt", txtbox_custom.Text);
            }
            await Task.Run(() =>
            {
                getDBSessions();
                getDBYears();
                getDBMonths();

                getDBAssetsFields();
                //    getDBSessions();
                //listStocks = getDBStocks();
                //foreach (Stock stock in listStocks)
                //{
                //insertStockPerformance(stock);
                //insertStockSaleMonth(stock);
                //insertStockCash(stock);
                //insertStockDividend(stock);
                //insertStockBalance(stock);
                //}
                //rule2Stocks = selectStockRule2();

                //rule3Stocks = selectStockRule3_1(6);
                if(myStocks.Count > 0)
                    selectStockRule3MyStock(3, myStocks);

            });
        }
    }

    public class CSVReader
    {
        private Stream objStream;
        private StreamReader objReader;

        //add name space System.IO.Stream
        public CSVReader()
        {

        }
        public CSVReader(Stream filestream) : this(filestream, null) { }
        public CSVReader(StreamReader strReader)
        {
            this.objReader = strReader;
        }
        public CSVReader(Stream filestream, Encoding enc)
        {
            this.objStream = filestream;
            //check the Pass Stream whether it is readable or not
            if (!filestream.CanRead)
            {
                return;
            }
            objReader = (enc != null) ? new StreamReader(filestream, enc) : new StreamReader(filestream);
        }
        //parse the Line
        public string[] GetCSVLine()
        {
            string data = objReader.ReadLine();
            if (data == null) return null;
            if (data.Length == 0) return new string[0];
            //System.Collection.Generic
            ArrayList result = new ArrayList();
            //parsing CSV Data
            ParseCSVData(result, data);
            return (string[])result.ToArray(typeof(string));
        }

        public void ParseCSVData(ArrayList result, string data)
        {
            int position = -1;
            while (position < data.Length)
                result.Add(ParseCSVField(ref data, ref position));
        }

        private string ParseCSVField(ref string data, ref int StartSeperatorPos)
        {
            if (StartSeperatorPos == data.Length - 1)
            {
                StartSeperatorPos++;
                return "";
            }

            int fromPos = StartSeperatorPos + 1;
            if (data[fromPos] == '"')
            {
                int nextSingleQuote = GetSingleQuote(data, fromPos + 1);
                int lines = 1;
                while (nextSingleQuote == -1)
                {
                    data = data + "\n" + objReader.ReadLine();
                    nextSingleQuote = GetSingleQuote(data, fromPos + 1);
                    lines++;
                    if (lines > 20)
                        throw new Exception("lines overflow: " + data);
                }
                StartSeperatorPos = nextSingleQuote + 1;
                string tempString = data.Substring(fromPos + 1, nextSingleQuote - fromPos - 1);
                tempString = tempString.Replace("'", "''");
                return tempString.Replace("\"\"", "\"");
            }

            int nextComma = data.IndexOf(',', fromPos);
            if (nextComma == -1)
            {
                StartSeperatorPos = data.Length;
                return data.Substring(fromPos);
            }
            else
            {
                StartSeperatorPos = nextComma;
                return data.Substring(fromPos, nextComma - fromPos);
            }
        }

        private int GetSingleQuote(string data, int SFrom)
        {
            int i = SFrom - 1;
            while (++i < data.Length)
                if (data[i] == '"')
                {
                    if (i < data.Length - 1 && data[i + 1] == '"')
                    {
                        i++;
                        continue;
                    }
                    else
                        return i;
                }
            return -1;
        }
    }

}

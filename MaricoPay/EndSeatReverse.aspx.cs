using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using WebLibs;
using System.Collections.Generic;
using WebLibs.DataContext;
using System.Linq;
using WebLibs.Memcached;

namespace Pennycms.Pages.SeatReverseAuction
{
    public partial class EndSeatReverse : Page
    {
        public string UrlRoot = DBCommon.UrlRootCMS;
        DMSDBDataContext db = new DMSDBDataContext();
        private dbProduct _product;
        private readonly DbOrder _order = new DbOrder();
        public string _AppCode = "product";
        private string Keyword = "";
        private const int RecordPerPage = 15;

        public string fCheck(object value)
        {
            if (value.ToString() == "0" || value.ToString() == "" || value.ToString() == "false")
            {
                return "";
            }
            else
            {
                return "checked";
            }
        }

        private int CurrPage
        {
            get { return ViewState["CurrPage"] != null ? (int)ViewState["CurrPage"] : 1; }
            set
            {
                ViewState["CurrPage"] = value;
            }
        }

        public bool fCheckRefunded(object id)
        {
            var refu = db.tbl_SeatAuctions.Where(p => p.ProductId.ToString() == id.ToString() && p.Refund == 1).ToList();
            return refu.Count > 0;
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            var siteMaster = (masters.SiteMaster)Master;
            if (siteMaster != null) siteMaster.AppCode = _AppCode;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;

            if (!IsPostBack)
            {
                if (DBCommon.TryParseInt(Request.QueryString["s"].ToString()) == 2)
                {
                    btnDelete.Visible = true;
                }
            }

            hfStatus.Value = Request.QueryString["s"];
            tbxParentName.Attributes.Add("style", "width:250px");
            tbxParentName.Attributes.Add("onclick", "javascript:ShowDialog(); return false;");
            tbxKeyword.Attributes.Add("style", "width:90%");
            BindData();
        }

        protected void ibtnCategoryIdChange_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                CurrPage = 1;
                BindData();
            }
            catch
            {
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            CurrPage = 1;
            BindData();
        }

        protected void dlPaper_ItemCreated(object sender, DataListItemEventArgs e)
        {
            if (e.Item.ItemIndex == -1 || e.Item.ItemType == ListItemType.Separator) return;
            var lbtPage = (LinkButton)e.Item.FindControl("lbtPage");
            lbtPage.CausesValidation = false;
            lbtPage.Click += lbtPageClick;
        }

        protected void dlPaper_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.ItemType != ListItemType.Item && e.Item.ItemType != ListItemType.AlternatingItem) return;
            var lbtPage = (LinkButton)e.Item.FindControl("lbtPage");
            var dr = (DataRowView)e.Item.DataItem;
            if (dr == null) return;
            int pType = Convert.ToInt32(dr["Type"]);
            if (pType == 0)
            {
                lbtPage.Text = dr["Text"].ToString();
                lbtPage.CssClass = dr["CssClass"].ToString();
                lbtPage.CommandArgument = dr["Page"].ToString();
            }
            else
            {
                lbtPage.CommandArgument = dr["Page"].ToString();
                var img = new Image
                {
                    BorderWidth = Unit.Pixel(0),
                    CssClass = "icon_paper",
                    ImageUrl = string.Format("~/icons/{0}", dr["Text"])
                };
                lbtPage.Controls.Add(img);
            }
        }

        protected void lbtPageClick(object sender, EventArgs e)
        {
            var lbt = (LinkButton)sender;
            if (lbt == null) return;
            CurrPage = DBCommon.TryParseInt(lbt.CommandArgument);
            BindData();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            foreach (RepeaterItem rptItem in rptProduct.Items)
            {
                var cbxSelect = (CheckBox)rptItem.FindControl("cbxSelect");
                if (!cbxSelect.Checked) continue;
                var hiddenID = (HiddenField)rptItem.FindControl("hiddenID");
                _product = new dbProduct();
                _product.DeleteIdClose(long.Parse(hiddenID.Value));
            }
            BindData();
        }

        protected void RptProductItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Header)
            {
                var cbxHeaderSelect = (CheckBox)e.Item.FindControl("cbxHeaderSelect");
                cbxHeaderSelect.Attributes.Add("style", "cursor:pointer");
                cbxHeaderSelect.Attributes.Add("onclick", "SelectAll('" + cbxHeaderSelect.ClientID + "', 'cbxSelect')");
                if (hfStatus.Value == "3")
                {
                    var tdSubspandTime = (HtmlTableCell)e.Item.FindControl("tdSubspandTime");
                    tdSubspandTime.Attributes.Add("style", "display");
                }

            }
            if (e.Item.ItemType != ListItemType.Item && e.Item.ItemType != ListItemType.AlternatingItem) return;
            var trItem = (HtmlTableRow)e.Item.FindControl("trItem");
            var hiddenID = (HiddenField)e.Item.FindControl("hiddenID");
            var hlView = (Literal)e.Item.FindControl("hlView");
            var hlDelete = (LinkButton)e.Item.FindControl("hlDelete");
            var hlViewWinner = (LinkButton)e.Item.FindControl("hlViewWinner");
            var hlClone = (Literal)e.Item.FindControl("hlClone");
            var hlReactive = (LinkButton)e.Item.FindControl("hlReactive");
            var hlSuspended = (LinkButton)e.Item.FindControl("hlSuspended");
            var hlEdit = (Literal)e.Item.FindControl("hlEdit");
            var lbStartTime = (Literal)e.Item.FindControl("lbStartTime");
            var lbEndTime = (Literal)e.Item.FindControl("lbEndTime");
            var lbCateName = (Literal)e.Item.FindControl("lbCateName");
            var hlTitle = (HtmlAnchor)e.Item.FindControl("hlTitle");
            var item = (DataRowView)e.Item.DataItem;
            if (item != null)
            {
                trItem.Attributes.Add("class", e.Item.ItemIndex % 2 == 1 ? "alter" : "item");

                hiddenID.Value = item["Id"].ToString();
                hlView.Text = string.Format("[ <a target='_blank' href='{0}{1}-{2}/detail.htm' class='treebook'>View</a> ]", DBConfig.DomainName, item["Id"], WebLibs.DBCommon.RemoveReservedCharacters(item["ProductName"].ToString().Replace(' ', '-')));
                hlTitle.HRef = string.Format("{0}{1}-{2}/detail.htm", DBConfig.DomainName, item["Id"], WebLibs.DBCommon.RemoveReservedCharacters(item["ProductName"].ToString().Replace(' ', '-')));
                if (Convert.ToInt64(item["WinnerId"]) != 0)
                {
                    hlViewWinner.Attributes.Add("onclick", string.Format("HistoryPreview('{0}');", item["Id"]));
                }
                else
                {
                    hlViewWinner.Visible = false;
                    hlViewWinner.Attributes.Add("style", "display:none");

                }
                lbStartTime.Text = DBCommon.TryParseDateTime(item["StartTime"].ToString()).AddSeconds(DBCommon.fTimeStampUser()).ToString();
                lbEndTime.Text = DBCommon.TryParseDateTime(item["EndTime"].ToString()).AddSeconds(DBCommon.fTimeStampUser()).ToString();
                lbCateName.Text = item["Name"].ToString();

                hlDelete.CommandArgument = item["Id"].ToString();
                hlReactive.CommandArgument = item["Id"].ToString();
                hlSuspended.CommandArgument = item["Id"].ToString();
                hlEdit.Text = string.Format("<a class='treebook' href='{0}product/edit/{1}/index.htm?from=" + hfStatus.Value + "'>" + "Edit</a>", UrlRoot, item["Id"]);
                if (item["IsSeatAuction"] != null && item["IsSeatAuction"].ToString() == "1")
                {
                    using (var db = new DMSDBDataContext())
                    {
                        var model = db.tbl_BuyNowSettings.SingleOrDefault(p => p.ProductId == DBCommon.TryParseInt(item["Id"].ToString()));

                        if (model == null)
                        {
                            hlEdit.Text = string.Format("<a class=\'treebook\' href=\'{0}product/{1}/editseatauctions.htm?from=" + hfStatus.Value + "\'>Edit</a>", UrlRoot, item["Id"]);
                        }
                        else
                        {
                            hlEdit.Text = string.Format("<a class=\'treebook\' href=\'{0}product/{1}/seatreverseauction.htm?from=" + hfStatus.Value + "\'>Edit</a>", UrlRoot, item["Id"]);
                        }
                    }
                }

                if (hfStatus.Value == "1")
                {
                    hlSuspended.Visible = true;
                    hlViewWinner.Visible = true;
                }
                if (hfStatus.Value == "2")
                {
                    hlDelete.Visible = true;
                    hlViewWinner.Visible = true;

                }
                if (hfStatus.Value == "3")
                {
                    hlReactive.Visible = true;
                    hlDelete.Visible = true;
                    hlEdit.Visible = true;
                    var tdSubSpand = (HtmlTableCell)e.Item.FindControl("tdSubSpand");
                    tdSubSpand.Attributes.Add("style", "display");
                    var lbSubspandTime = (Literal)e.Item.FindControl("lbSubspandTime");
                    lbSubspandTime.Text = item["SubSpendTime"].ToString();
                }

                var _SeatProperties = new SeatProperties { ProductId = Convert.ToInt32(item["Id"]) };
                var itemseat = _SeatProperties.Get(_SeatProperties.ProductId);

                #region Clone
                if (itemseat != null)
                {
                    hlClone.Text = string.Format("<a class='treebook' href='{0}product/{1}/editseatauctions.htm?clone=ok&from=" + hfStatus.Value + "'>Clone</a>", UrlRoot, item["Id"]);

                    var model = db.tbl_BuyNowSettings.SingleOrDefault(p => p.ProductId == Convert.ToInt32(item["Id"]));

                    if (model != null)
                    {
                        hlClone.Text = string.Format("<a class='treebook' href='{0}product/{1}/seatreverseauction.htm?clone=ok&from=" + hfStatus.Value + "'>Clone</a>", UrlRoot, item["Id"]);
                    }
                }
                else
                {
                    hlClone.Text = string.Format("<a class='treebook' href='{0}product/edit/{1}/index.htm?clone=ok&from=" + hfStatus.Value + "'>Clone</a>", UrlRoot, item["Id"]);
                }
                #endregion
            }
        }

        public string GetAuctionType(object objType)
        {
            var p = objType as DataRowView;
            if (p != null)
            {
                if (DBCommon.TryParseInt(p["isNewSeatAuction"].ToString()) > 0)
                    return "Seat Reverse Auction";
                if (DBCommon.TryParseInt(p["IsSeatAuction"].ToString()) > 0)
                    return "Seat Auction";
                if (DBCommon.TryParseFloat(p["GiveAway"].ToString()) > 0)
                    return "Give Away";
                if (DBCommon.TryParseFloat(p["MiniumPrice"].ToString()) > 0)
                    return "Minimum Price";
                if (DBCommon.TryParseInt(p["IsGoldenTime"].ToString()) > 0)
                    return "Golden Time";
                if (DBCommon.TryParseFloat(p["BuyReward"].ToString()) > 0)
                    return "Buy Now Earned Reward ";
            }
            return "Regular";
        }

        private void BindData()
        {
            if (hddParentID.Value.Trim().Length > 0)
            {
                DBCommon.TryParseInt(hddParentID.Value);
            }
            if (tbxKeyword.Text.Trim().Length > 0)
            {
                Keyword = tbxKeyword.Text.Trim();
            }

            _product = new dbProduct();
            int TotalRecord;
            DataTable list_product = 
                _product.GetListProduct(Keyword.Trim(), DBCommon.TryParseInt(hddParentID.Value), DBCommon.TryParseInt(txtAuctionId.Text), 
                6, DBCommon.TryParseInt(hfStatus.Value), CurrPage, 
                RecordPerPage, out TotalRecord);
            rptProduct.DataSource = list_product;
            rptProduct.DataBind();
            if (list_product.Rows.Count > 0)
            {
                TotalRecord = DBCommon.TryParseInt(list_product.Rows[0]["TotalRecord"].ToString());
                if (TotalRecord > 0)
                {
                    int Recordcount = TotalRecord;
                    ltlTotal.Visible = dlPaper.Visible = dlPaper1.Visible = true;
                    ltlTotal1.Text = ltlTotal.Text = string.Format("Found {0}  record(s).", Recordcount.ToString());
                    int TotalPage = Recordcount / RecordPerPage;
                    if (Recordcount % RecordPerPage > 0)
                        TotalPage += 1;
                    DataTable dtPaper = Paper.MakeDataPaper(TotalPage, CurrPage, RecordPerPage);
                    dlPaper.DataSource = dtPaper;
                    dlPaper.DataBind();
                    dlPaper1.DataSource = dtPaper;
                    dlPaper1.DataBind();
                }
            }
            else
            {
                ltlTotal1.Text = "Found 0  record.";
                ltlTotal.Visible = dlPaper.Visible = dlPaper1.Visible = false;
            }
        }

        protected void rptData_ItemCreated(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemIndex != -1 && e.Item.ItemType != ListItemType.Separator)
            {
                var hlDelete = (LinkButton)e.Item.FindControl("hlDelete");
                hlDelete.CausesValidation = false;
                hlDelete.Attributes.Add("onclick", "return confirm(\"Do you really want to delete?\");");
                hlDelete.Click += hlDelete_Click;

                var hlSuspended = (LinkButton)e.Item.FindControl("hlSuspended");
                hlSuspended.CausesValidation = false;
                hlSuspended.Attributes.Add("onclick", "return confirm(\"Do you really want to Suspend ?\");");
                hlSuspended.Click += hlSuspended_Click;

                var hlReactive = (LinkButton)e.Item.FindControl("hlReactive");
                hlReactive.CausesValidation = false;
                hlReactive.Attributes.Add("onclick", "return confirm(\"Do you really want to ReActive?\");");
                hlReactive.Click += hlSuspended_Click;

                var hlClosed = (LinkButton)e.Item.FindControl("hlClosed");
                if (hfStatus.Value == "3")
                {
                    hlClosed.CausesValidation = false;
                    hlClosed.Attributes.Add("onclick", string.Format("return confirmClose({0});", DataBinder.Eval(e.Item.DataItem, "Id")));
                    //hlClosed.Click += hlInActive;
                }
                else
                {
                    hlClosed.Visible = false;
                }         
            }
        }

        public string GetOpenIssue(int type, object productid)
        {

            List<DbOrder> list = _order.GetOrderbyProductId(long.Parse(productid.ToString()));
            if (list != null && list.Count > 0)
            {
                foreach (DbOrder item in list)
                {
                    if (item.Status == type)
                        return "Yes";
                }
            }
            return "No";
        }

        protected void hlDelete_Click(object sender, EventArgs e)
        {
            var lBtn = (LinkButton)sender;
            if (lBtn != null)
            {
                var pro = new dbProduct();
                pro.Delete(long.Parse(lBtn.CommandArgument));
                BindData();
            }
        }

        protected void hlSuspended_Click(object sender, EventArgs e)
        {
            var lBtn = (LinkButton)sender;
            if (lBtn != null)
            {               
                if (DBCommon.fLimitProduct())
                {
                    hfError.Value = "13";
                    return;
                }
                //Ko cho Suspended khi sold
                var item = db.tbl_Products.SingleOrDefault(p => p.Id == int.Parse(lBtn.CommandArgument));
                if (item != null && (item.Status == 2 || item.Status == 4))
                {
                    hfError.Value = "14";
                    return;
                }

                #region Check ReActive
                if (item.Status == 3 && item.EndTime <= db.spLoad_DateTime().FirstOrDefault().DateTimeDatabase)
                {
                    hfError.Value = "15";
                    return;
                }
                #endregion

                var pro = new dbProduct();
                pro.UpdateStatus(long.Parse(lBtn.CommandArgument));
                var model = db.tbl_UserViewCurrents.Where(p => p.ProductId == int.Parse(lBtn.CommandArgument));
                if (model != null)
                {
                    db.tbl_UserViewCurrents.DeleteAllOnSubmit(model);
                    db.SubmitChanges();
                }

                if (Request.QueryString["s"] == "1")
                {
                    // Cached to Refresh Status live site 
                    using (var da = new DMSDBDataContext())
                    {
                        #region Add Table Cache
                        var proc_cache = new tbl_ProductActive
                        {
                            ProductId = int.Parse(lBtn.CommandArgument),
                            DateTimeActive = DBCommon.TryParseDateTime(db.spLoad_DateTime().FirstOrDefault().DateTimeDatabase.ToString()),
                            Status_ = 0,
                            Type = "Suppend"
                        };
                        da.tbl_ProductActives.InsertOnSubmit(proc_cache);
                        da.SubmitChanges();
                        #endregion
                    }
                }

                if (Request.QueryString["s"] == "3")
                {
                    using (var da = new DMSDBDataContext())
                    {
                        #region Add Table Cache
                        var proc_cache = new tbl_ProductActive
                        {
                            ProductId = int.Parse(lBtn.CommandArgument),
                            DateTimeActive = DBCommon.TryParseDateTime(db.spLoad_DateTime().FirstOrDefault().DateTimeDatabase.ToString()),
                            Status_ = 0,
                            Type = "Suppend"
                        };
                        da.tbl_ProductActives.InsertOnSubmit(proc_cache);
                        da.SubmitChanges();
                        #endregion

                        #region Reset seat
                        var mail = da.tbl_EmailSents.Where(p => p.ProductId == int.Parse(lBtn.CommandArgument));
                        if (mail.ToList().Count > 0)
                        {
                            da.tbl_EmailSents.DeleteAllOnSubmit(mail);
                            da.SubmitChanges();
                        }

                        var setting = da.tbl_SettingAutoStarts.FirstOrDefault(p => p.ProductId == int.Parse(lBtn.CommandArgument));
                        if (setting != null)
                        {
                            setting.IsXOk = 0;
                            setting.IsYOk = 0;
                            da.SubmitChanges();
                        }

                        var proc = da.tbl_Products.FirstOrDefault(p => p.Id == int.Parse(lBtn.CommandArgument));
                        if (setting != null)
                        {
                            proc.IsDelay = 0;
                            da.SubmitChanges();
                        }
                        #endregion
                    }
                }
                BindData();
            }
        }

        protected void hlInActive(object sender, EventArgs e)
        {
            var lBtn = (LinkButton)sender;
            if (lBtn != null)
            {
                var OldId = DBCommon.TryParseInt(lBtn.CommandArgument);
                var _Seat = new SeatProperties { ProductId = OldId };
                _Seat.CloseRefund(OldId, 2, 0);
                // Cached to Refresh Status live site 
                List<int> list;
                if (CacheUtil.GetCache(Constants.SeatClosedRefund) != null)
                    list = CacheUtil.GetCache(Constants.SeatClosedRefund) as List<int> ?? new List<int>();
                else
                    list = new List<int>();
                list.Add(OldId);
                CacheUtil.SetCacheWithTime(Constants.SeatClosedRefund, list, 0.1);
                BindData();
            }
        }

        protected void rptProduct_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "hlClosed")
            {
                var OldId = DBCommon.TryParseInt(e.CommandArgument.ToString());
                var _Seat = new SeatProperties { ProductId = OldId };
                _Seat.CloseRefund(OldId, 2, 0);
                // Cached to Refresh Status live site 
                using (var db = new DMSDBDataContext())
                {
                    #region Add Table Cache
                    var proc_cache = new tbl_ProductActive
                    {
                        ProductId = OldId,
                        DateTimeActive = DBCommon.TryParseDateTime(db.spLoad_DateTime().FirstOrDefault().DateTimeDatabase.ToString()),
                        Status_ = 0,
                        Type = "Suppend"
                    };
                    db.tbl_ProductActives.InsertOnSubmit(proc_cache);
                    db.SubmitChanges();
                    #endregion
                }

                BindData();
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IPDetectServer.Web
{
    public static class ControlHelper
    {
        public static MvcHtmlString PagingControl(string name, int totalRecords, int currentPage, int recordPerPage)
        {
            int remainRecord = totalRecords % recordPerPage;
            int intTotalPage = totalRecords / recordPerPage;
            if (remainRecord != 0)
            {
                intTotalPage++;
            }

            string totalPage = intTotalPage.ToString();
            string prePage = (currentPage == 0 ? "0" : (currentPage - 1).ToString());
            string nextPage = ((currentPage + 1) < intTotalPage ? (currentPage + 1).ToString() : (currentPage).ToString());
            string lastPage = (intTotalPage - 1).ToString();


            string content = @"<script type=""text/javascript"">
    function navigate(number) {
        $(""#txtPageNaviNo"").val(number);                
        $('#" + name + @"').click();
    }
</script>
<table class='mobile-grid-tb-paging'> 
    <tr>
        <td style='padding:0'><span class=""sp-count"">共&nbsp;&nbsp;" + totalRecords + @"&nbsp;&nbsp;条</span>
            <a href=""javascript:void(0)"" onclick='javascript:navigate(""0"");' >|<<</a>&nbsp;
            <a href=""javascript:void(0)"" onclick='javascript:navigate(""" + prePage + @""");' ><</a>&nbsp;
            <input type=""text"" value=""" + (currentPage + 1).ToString() + @""" class=""mobile-grid-tb-paging-txt-currentpage"" />&nbsp;of&nbsp;<span id=""lblTotalPage"">" + totalPage + @"</span>&nbsp;页&nbsp;
            <a href=""javascript:void(0)"" onclick=""javascript:navigate('" + nextPage + @"');"" >></a>&nbsp;
            <a href=""javascript:void(0)"" onclick='javascript:navigate(""" + lastPage + @""");' >>>|</a>&nbsp;
            <input type=""text"" name=""txtPageNaviNo"" id=""txtPageNaviNo"" value="""" style=""display:none;""/>                    
            <input type=""submit"" name=""" + name + @""" id=""" + name + @""" style=""display:none"" />                        
        </td>
    </tr>
</table>";

            return new MvcHtmlString(content);

        }

//        public static MvcHtmlString SubMenuControl(string RootPath)
//        {
//            string content = @"<script src=" + RootPath + @"/Scripts/jquery.lavalamp.js"" type=""text/javascript""></script>
//<script src=" + RootPath + @"/Scripts/jquery.easing.js"" type=""text/javascript""></script>
//<script type=""text/javascript"">
//    $(function () { $("".submenu"").lavaLamp({ fx: ""backout"", speed: 700 }) });
//</script>";
//            content += "<ul class='submenu'>";

            
//            content += "</ul>";
//            return new MvcHtmlString(content);
//        }
    }
}
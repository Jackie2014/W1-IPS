using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AttributeRouting;
using IPDetectServer.Models;
using IPDetectServer.Repositories;
using IPDetectServer.Lib.Exceptions;
using AttributeRouting.Web.Http;
using System.Web.Http;
using IPDetectServer.Lib;
using IPDetectServer.Models.WS;

namespace IPDetectServer.Services.V1
{
    [RoutePrefix("apis/v1/cidrsettings")]
    public class CIDRSettingsController : ApiController
    {
        [POST("{id}/update")]
        public AjaxResponse Update([FromBody] CIDRSettingModel model, string id)
        {
            AjaxResponse response = new AjaxResponse();
            response.IsSuccess = true;
            try
            {
                if (model == null
                    || String.IsNullOrWhiteSpace(model.IPStart)
                    || String.IsNullOrWhiteSpace(model.IPEnd)
                    || model.TCPFaZhi <= 0
                    || model.TCPPort <= 0
                    || model.TTLFaZhi <= 0
                    )
                {
                    response.IsSuccess = false;
                    response.Message = "请求的数据格式不正确。";
                    return response;
                    //throw new BadRequestException("请求的数据不能为空。");
                }

                model.ID = id;
                model.LastUpdatedBy = Context.LoginName;
                CIDRSettingRepository rep = new CIDRSettingRepository();
                rep.UpdateSetting(model);

            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }

            return response;
        }

        [DELETE("{settingid}")]
        public AjaxResponse DeleteSetting(string settingid)
        {
            AjaxResponse response = new AjaxResponse();
            response.IsSuccess = true;
            try
            {
                if (string.IsNullOrWhiteSpace(settingid))
                {
                    response.IsSuccess = false;
                    response.Message = "settingid 不能为空.";
                    return response;
                }
                CIDRSettingRepository rep = new CIDRSettingRepository();
                rep.DeleteSetting(settingid);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }

            return response;
        }

        [GET("")]
        public List<CIDRSettingModel> GetIPSettingList()
        {
            CIDRSettingRepository rep = new CIDRSettingRepository();
            var result = rep.GetSettings(0, 10000);
            return result;
        }
    }
}

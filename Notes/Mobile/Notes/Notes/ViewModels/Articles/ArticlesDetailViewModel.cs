using Microsoft.AppCenter.Crashes;
using Notes.Helpers;
using Notes.Models;
using Notes.Models.Articles;
using Notes.Services;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Notes.ViewModels
{
    public class ArticlesDetailViewModel : BaseViewModel
    {
        #region 属性

        private ArticlesDto articles;

        public ArticlesDto Articles
        {
            get => articles;
            set
            {
                articles = value;
                RaisePropertyChanged(() => Articles);
            }
        }

        bool hasError;
        public bool HasError
        {
            get => hasError;
            set
            {
                hasError = value;
                RaisePropertyChanged(() => HasError);
            }
        }

        #endregion

        #region 方法

        public ArticlesDetailViewModel(ArticlesDto entity)
        {
            Articles = entity;
            IsBusy = true;
        }  

        /// <summary>
        /// 从api刷新数据，把获取的数据绑定到页面，并且保存在Sqlite中
        /// </summary>
        /// <returns></returns>
        public async Task<ArticlesDto> GetMessageAsync()
        {
            try
            { 
                IsBusy = true;
                HasError = false;  
                LoadStatus = LoadMoreStatus.StausLoading;
                ArticlesDto baseMessageEntity = await ServicesManager.ArticlesService.GetArticlesFromSqlite(Articles.Id);
                if (baseMessageEntity != null)
                {
                    LoadStatus = LoadMoreStatus.StausDefault;
                    Articles = baseMessageEntity;  
                    return baseMessageEntity;
                }
                return null;
                //MobileMessageGetParam messageGetParam = new MobileMessageGetParam();
                //messageGetParam.PushType = Message.PushType;
                //messageGetParam.MessageKey = UserInfoSetting.Current.UserKey;
                //var result = await ServicesManager.MessageService.GetMessage(messageGetParam);
                //if (!result.IsSuccess)
                //{
                //    HasError = true;
                //    LoadStatus = LoadMoreStatus.StausError;
                //    Crashes.TrackError(new Exception() { Source = result.Message }); 
                //    return null;
                //}
                //if (result.Data == null)
                //{
                //    LoadStatus = LoadMoreStatus.StausNodata;
                //    HasError = true; 
                //    return null;
                //}
                //LoadStatus = LoadMoreStatus.StausDefault;
                //Message = result.Data;
                //await UpdateMessageIsRead(result.Data);
                //return result.Data; 
            }
            catch (Exception ex)
            {
                HasError = true;
                Crashes.TrackError(ex);
                return null;
            }
            finally
            {
                IsBusy = false;
            }
        }

         

        #endregion
    } 
}

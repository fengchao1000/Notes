using Notes.Helpers;
using Notes.Models;
using Notes.Models.Categorys;
using Notes.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Notes.ViewModels.Categorys
{
    public class CategoryViewModel : BaseViewModel
    {
        #region 属性

        /// <summary>
        /// Category集合
        /// </summary>
        private ObservableCollection<Category> categorys = new ObservableCollection<Category>();
        public ObservableCollection<Category> Categorys
        {
            get => categorys;
            set
            {
                categorys = value;
                RaisePropertyChanged(() => Categorys);
            }
        }

        /// <summary>
        /// 滑动操作的ItemIndex
        /// </summary>
        internal int ItemIndex
        {
            get;
            set;
        }

        #endregion

        #region 方法

        /// <summary>
        /// 初始化ViewModel
        /// </summary>
        public async void Initialize()
        {
            string refreshKey = nameof(CategoryViewModel);
            if (DateTime.UtcNow >= RefreshTimeHelper.GetRefreshTime(refreshKey))
            {
                await RefreshDataFromAPIAsync();
            }
            else
            {
                await GetDataFromSqliteAsync();
            }
        }

        /// <summary>
        /// 从Sqlite中获取数据
        /// </summary>
        public async Task GetDataFromSqliteAsync()
        {
            try
            {
                IsBusy = true; 
                Stopwatch sw = new Stopwatch();
                 
                IList<Category> categoryList = await ServicesManager.CategoryService.GetAllFromSqliteAsync();

                if (categoryList != null)
                {
                    Categorys.Clear();

                    ObservableCollection<Category> newCategoryCollection = new ObservableCollection<Category>();

                    foreach (Category item in categoryList)
                    {
                        newCategoryCollection.Add(item);
                    }

                    sw.Start();

                    Categorys = newCategoryCollection;
                }

                sw.Stop();
                LoggerHelper.Current.Debug("EbayMsgMenuViewModel GetDataFromSqliteAsync ElapsedMilliseconds:" + sw.ElapsedMilliseconds);
            }
            catch (Exception ex)
            {
                LoggerHelper.Current.Error(ex.ToString());
            }
            finally
            {
                await Task.Delay(ConstanceHelper.RefreshDelay);
                IsBusy = false;
            }
        }

        async Task RefreshDataFromAPIAsync()
        {
            try
            {
                IsBusy = true;

                //查询目录对应数量 
                var result = await ServicesManager.CategoryService.GetCategory("", "", 1);
                if (!result.IsSuccess)
                {
                    return;
                }

                if (result.Data.Items == null)
                {
                    return;
                }

                if (result.Data.Items.Count <= 0)
                {
                    return;
                }

                Categorys.Clear();

                foreach (Category item in result.Data.Items)
                {
                    Categorys.Add(item);
                }

                await ServicesManager.CategoryService.BatchUpdateToSqlite(Categorys); //保存在sqlite

                LoadStatus = LoadMoreStatus.StausDefault;
                CanLoadMore = true;
            }
            catch (Exception ex)
            {
                LoggerHelper.Current.Error(ex.ToString());
                LoadStatus = LoadMoreStatus.StausFail;
            }
            finally
            {
                string refreshKey = nameof(CategoryViewModel);
                RefreshTimeHelper.SetRefreshTime(refreshKey, DateTime.UtcNow.AddMinutes(ConstanceHelper.NextRefreshInterval));//记录刷新时间 
                await Task.Delay(ConstanceHelper.RefreshDelay);
                IsBusy = false;
            }
        }

        #endregion

        #region 命令 

        /// <summary>
        /// 刷新命令
        /// </summary>
        private ICommand refreshCommand;
        public ICommand RefreshCommand => refreshCommand ?? (refreshCommand = new Command(async () => await RefreshDataFromAPIAsync()));

        #endregion
    }
}

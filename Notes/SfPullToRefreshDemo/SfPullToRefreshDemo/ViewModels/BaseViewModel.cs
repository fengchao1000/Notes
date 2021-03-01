using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using Xamarin.Forms;

namespace SfPullToRefreshDemo.ViewModels
{
    public abstract class BaseViewModel : BindableObject
    {  

        /// <summary>
        /// 加载数据
        /// </summary>
        public virtual void InitializeAsync()
        {
        }
         
         

        bool isBusy;

        /// <summary>
        /// Gets or sets a value indicating whether this instance is busy.
        /// </summary>
        /// <value><c>true</c> if this instance is busy; otherwise, <c>false</c>.</value>
        public bool IsBusy
        {
            get => isBusy;
            set
            {
                isBusy = value;
                RaisePropertyChanged(() => IsBusy);
            }
        }

        bool canLoadMore = false;

        /// <summary>
        /// Gets or sets a value indicating whether this instance can load more.
        /// </summary>
        /// <value><c>true</c> if this instance can load more; otherwise, <c>false</c>.</value>
        public bool CanLoadMore
        {
            get => canLoadMore;
            set
            {
                canLoadMore = value;
                RaisePropertyChanged(() => CanLoadMore);
            }
        }

        public void RaisePropertyChanged<T>(Expression<Func<T>> property)
        {
            var name = GetMemberInfo(property).Name;
            OnPropertyChanged(name);
        }

        bool hasData = true;
        /// <summary>
        /// HasData
        /// </summary>
        public bool HasData
        {
            get => hasData;
            set
            {
                hasData = value;
                RaisePropertyChanged(() => HasData);
            }
        }

        bool noData = false;
        /// <summary>
        /// NoData
        /// </summary>
        public bool NoData
        {
            get => noData;
            set
            {
                noData = value;
                RaisePropertyChanged(() => NoData);
            }
        }

        bool noPermission = false;
        /// <summary>
        /// NoPermission
        /// </summary>
        public bool NoPermission
        {
            get => noPermission;
            set
            {
                noPermission = value;
                RaisePropertyChanged(() => NoPermission);
            }
        }
         

        private MemberInfo GetMemberInfo(Expression expression)
        {
            MemberExpression operand;
            LambdaExpression lambdaExpression = (LambdaExpression)expression;
            if (lambdaExpression.Body as UnaryExpression != null)
            {
                UnaryExpression body = (UnaryExpression)lambdaExpression.Body;
                operand = (MemberExpression)body.Operand;
            }
            else
            {
                operand = (MemberExpression)lambdaExpression.Body;
            }
            return operand.Member;
        }

    }
}

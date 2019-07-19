using Notes.Helpers;
using System; 
using System.Linq.Expressions;
using System.Reflection;
using Xamarin.Forms;

namespace Notes.ViewModels
{
    public abstract class BaseViewModel : BindableObject
    {

        LoadMoreStatus loadStatus;
        public LoadMoreStatus LoadStatus
        {  
            get => loadStatus;
            set
            {
                loadStatus = value;
                RaisePropertyChanged(() => LoadStatus);
            } 
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

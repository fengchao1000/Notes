using System;
using System.Collections.Generic;
using System.Text;

namespace SfPullToRefreshDemo.ViewModels
{
    public class Page1ViewModel : BaseViewModel
    {
        /// <summary>
        /// Progress
        /// </summary>
        private double progress = 0;
        public double Progress
        {
            get => progress;
            set
            {
                progress = value;
                RaisePropertyChanged(() => Progress);
            }
        }

        /// <summary>
        /// ProgressText
        /// </summary>
        private double progressText = 0;
        public double ProgressText
        {
            get => progressText;
            set
            {
                progressText = value;
                RaisePropertyChanged(() => ProgressText);
            }
        }
    }
}

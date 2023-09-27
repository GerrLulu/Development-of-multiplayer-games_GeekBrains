using System.Threading;
using System.Threading.Tasks;
using TMPro;

namespace Lesson5
{
    public class LoadingUi
    {
        private TMP_Text _titleLabel;
        private CancellationTokenSource cancelTokenSource;
        private CancellationToken cancelToken;


        public LoadingUi(TMP_Text titleLable)
        {
            _titleLabel = titleLable;
        }

        public void StartLoad()
        {
            cancelTokenSource = new CancellationTokenSource();
            cancelToken = cancelTokenSource.Token;

            PrintLoad(cancelToken, 200);
        }

        public void StopLoad()
        {
            cancelTokenSource.Cancel();
            cancelTokenSource.Dispose();
        }

        private async void PrintLoad(CancellationToken cancelToken, int time)
        {
            while (!cancelToken.IsCancellationRequested)
            {
                _titleLabel.text = "Loading .";
                await Task.Delay(time);
                _titleLabel.text = "Loading ..";
                await Task.Delay(time);
                _titleLabel.text = "Loading ...";
                await Task.Delay(time);
            }

            _titleLabel.text = "";
        }
    }
}
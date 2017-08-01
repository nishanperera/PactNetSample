using PactNet.Reporters.Outputters;

namespace Provider.Tests
{
    public class NUnitOutput : IReportOutputter
    {
        public void Write(string report)
        {
            System.Diagnostics.Debug.WriteLine(report);
        }
    }
}
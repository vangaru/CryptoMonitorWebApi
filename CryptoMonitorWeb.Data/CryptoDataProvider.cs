using AlphaVantage.Net.Common.Currencies;
using AlphaVantage.Net.Common.Exceptions;
using AlphaVantage.Net.Common.Intervals;
using AlphaVantage.Net.Core.Client;
using AlphaVantage.Net.Crypto;
using AlphaVantage.Net.Crypto.Client;
using CryptoMonitorWeb.Domain.Data;

namespace CryptoMonitorWeb.Data;

public class CryptoDataProvider : IDisposable, ICryptoDataProvider
{
    private const string AlphaVantageApiKeyConfigSection = "AlphaVantageApiKey";
    
    private readonly CryptoClient _cryptoClient = new AlphaVantageClient(AlphaVantageApiKeyConfigSection).Crypto();

    public async Task<List<CryptoTimeSeries>> GetCryptoTimeSeriesAsync(IEnumerable<DigitalCurrency> currencies, 
        CancellationToken cancellationToken)
    {
        var timeSeriesList = new List<CryptoTimeSeries>();
        
        foreach (var currency in currencies)
        {
            try
            {
                CryptoTimeSeries timeSeries = await GetCryptoTimeSeriesAsync(currency, cancellationToken);
                timeSeriesList.Add(timeSeries);
            }
            catch (AlphaVantageException)
            {
                // ignore
            }
        }

        return timeSeriesList;
    }

    public Task<CryptoTimeSeries> GetCryptoTimeSeriesAsync(DigitalCurrency currency, CancellationToken cancellationToken)
    {
        return _cryptoClient.GetTimeSeriesAsync(currency, PhysicalCurrency.RUB, Interval.Daily);
    }

    public void Dispose()
    {
        _cryptoClient.Dispose();
    }
}
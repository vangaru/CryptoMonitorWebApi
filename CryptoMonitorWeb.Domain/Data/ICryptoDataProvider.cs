using AlphaVantage.Net.Common.Currencies;
using AlphaVantage.Net.Crypto;

namespace CryptoMonitorWeb.Domain.Data;

public interface ICryptoDataProvider
{
    public Task<List<CryptoTimeSeries>> GetCryptoTimeSeriesAsync(IEnumerable<DigitalCurrency> currencies, 
        CancellationToken cancellationToken);

    public Task<CryptoTimeSeries> GetCryptoTimeSeriesAsync(DigitalCurrency currency,
        CancellationToken cancellationToken);
}
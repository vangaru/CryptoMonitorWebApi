using AlphaVantage.Net.Common.Currencies;
using AlphaVantage.Net.Crypto;
using CryptoMonitorWeb.Domain.Data;
using MediatR;

namespace CryptoMonitorWeb.Domain.Queries.Handlers;

public class CryptoTimeSeriesQueryByNameHandler : IRequestHandler<CryptoTimeSeriesQueryByName, CryptoTimeSeries>
{
    private readonly ICryptoDataProvider _cryptoDataProvider;

    public CryptoTimeSeriesQueryByNameHandler(ICryptoDataProvider cryptoDataProvider)
    {
        _cryptoDataProvider = cryptoDataProvider;
    }

    public Task<CryptoTimeSeries> Handle(CryptoTimeSeriesQueryByName request, CancellationToken cancellationToken)
    {
        DigitalCurrency currency = Enum.Parse<DigitalCurrency>(request.CurrencyName, true);
        return _cryptoDataProvider.GetCryptoTimeSeriesAsync(currency, cancellationToken);
    }
}
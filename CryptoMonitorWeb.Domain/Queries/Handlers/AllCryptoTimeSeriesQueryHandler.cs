using System.Text.Json;
using AlphaVantage.Net.Common.Currencies;
using AlphaVantage.Net.Crypto;
using CryptoMonitorWeb.Domain.Data;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace CryptoMonitorWeb.Domain.Queries.Handlers;

public class AllCryptoTimeSeriesQueryHandler : IRequestHandler<AllCryptoTimeSeriesQuery, List<CryptoTimeSeries>>
{
    private const string ConfiguredCurrenciesFilePathConfigSection = "ConfiguredCurrenciesFilePath";
    
    private readonly ICryptoDataProvider _cryptoDataProvider;
    private readonly IConfiguration _configuration;

    private SortedSet<DigitalCurrency>? _configuredCurrencies;

    public AllCryptoTimeSeriesQueryHandler(IConfiguration configuration, ICryptoDataProvider cryptoDataProvider)
    {
        _configuration = configuration;
        _cryptoDataProvider = cryptoDataProvider;
    }

    public async Task<List<CryptoTimeSeries>> Handle(AllCryptoTimeSeriesQuery _, CancellationToken cancellationToken)
    {
        SortedSet<DigitalCurrency> configuredCurrencies = await GetConfiguredCurrenciesAsync();
        return await _cryptoDataProvider.GetCryptoTimeSeriesAsync(configuredCurrencies, cancellationToken);
    }
    
    private async Task<SortedSet<DigitalCurrency>> GetConfiguredCurrenciesAsync()
    {
        if (_configuredCurrencies == null)
        {
            string configuredCurrenciesFilePath = _configuration[ConfiguredCurrenciesFilePathConfigSection] 
                                                  ?? throw new ApplicationException("Currencies are not configured.");
            string configuredCurrenciesText = await File.ReadAllTextAsync(configuredCurrenciesFilePath);
            _configuredCurrencies = new SortedSet<DigitalCurrency>((JsonSerializer.Deserialize<string[]>(configuredCurrenciesText)
                                                                   ?? throw new ApplicationException($"Failed to deserialize contents of the file {configuredCurrenciesFilePath}")).Select(
                c => Enum.Parse<DigitalCurrency>(c, true)));
        }

        return _configuredCurrencies;
    }
}
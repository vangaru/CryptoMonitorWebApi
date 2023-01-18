using AlphaVantage.Net.Common.Currencies;
using AlphaVantage.Net.Crypto;

namespace CryptoMonitorWeb.Api.Dto;

public static class DtoProvider
{
    private const string DigitalCurrencyCodeKey = "Digital Currency Code";
    private const string DigitalCurrencyNameKey = "Digital Currency Name";
    private const string MarketCodeKey = "Market Code";
    private const string MarketNameKey = "Market Name";
    private const string LastRefreshedKey = "Last Refreshed";
    private const string TimeZoneKey = "Time Zone";
    private const string InformationKey = "Information";

    public static List<CryptoCurrencyDto> BuildCryptoCurrencyDto(IEnumerable<CryptoTimeSeries> timeSeries)
    {
        var cryptoCurrencyDtoList = new List<CryptoCurrencyDto>();
        foreach (CryptoTimeSeries ts in timeSeries)
        {
            cryptoCurrencyDtoList.Add(BuildCryptoCurrencyDto(ts));
        }

        return cryptoCurrencyDtoList;
    }

    public static CryptoCurrencyDto BuildCryptoCurrencyDto(CryptoTimeSeries timeSeries)
    {
        CryptoDataPoint dataPoint = timeSeries.DataPoints.First();
        var metadataDto = new CryptoCurrencyMetadataDto(
            timeSeries.MetaData[DigitalCurrencyCodeKey],
            timeSeries.MetaData[DigitalCurrencyNameKey],
            timeSeries.MetaData[MarketCodeKey],
            timeSeries.MetaData[MarketNameKey],
            timeSeries.MetaData[LastRefreshedKey],
            timeSeries.MetaData[TimeZoneKey],
            timeSeries.MetaData[InformationKey]);

        var dataPointDto = new DataPointDto(
            dataPoint.ClosingPrice,
            dataPoint.ClosingPrice,
            dataPoint.HighestPrice,
            dataPoint.HighestPriceUSD,
            dataPoint.LowestPrice,
            dataPoint.LowestPriceUSD,
            dataPoint.MarketCapitalization,
            dataPoint.OpeningPrice,
            dataPoint.OpeningPriceUSD,
            dataPoint.Time,
            dataPoint.Volume);

        var cryptoCurrencyDto = new CryptoCurrencyDto(timeSeries.FromCurrency.ToString(), timeSeries.ToCurrency.ToString(), dataPointDto, metadataDto);
        return cryptoCurrencyDto;
    }
}

public record CryptoCurrencyDto(
    string FromCurrency,
    string ToCurrency,
    DataPointDto DataPointDto,
    CryptoCurrencyMetadataDto CryptoCurrencyMetadataDto
);

public record DataPointDto(
    decimal ClosingPrice,
    decimal ClosingPriceUsd,
    decimal HighestPrice,
    decimal HighestPriceUsd,
    decimal LowestPrice,
    decimal LowestPriceUsd,
    decimal MarketCapitalization,
    decimal OpeningPrice,
    decimal OpeningPriceUsd,
    DateTime Time,
    decimal Volume
);

public record CryptoCurrencyMetadataDto(
    string DigitalCurrencyCode,
    string DigitalCurrencyName,
    string MarketCode,
    string MarketName,
    string LastRefreshed,
    string TimeZone,
    string Information
);
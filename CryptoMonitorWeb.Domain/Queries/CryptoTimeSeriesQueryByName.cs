using AlphaVantage.Net.Crypto;
using MediatR;

namespace CryptoMonitorWeb.Domain.Queries;

public record CryptoTimeSeriesQueryByName(string CurrencyName) : IRequest<CryptoTimeSeries>;
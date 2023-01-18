using AlphaVantage.Net.Crypto;
using MediatR;

namespace CryptoMonitorWeb.Domain.Queries;

public record AllCryptoTimeSeriesQuery : IRequest<List<CryptoTimeSeries>>;
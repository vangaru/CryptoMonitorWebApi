using AlphaVantage.Net.Crypto;
using CryptoMonitorWeb.Api.Dto;
using CryptoMonitorWeb.Domain.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CryptoMonitorWeb.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class CryptoController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public CryptoController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<List<CryptoCurrencyDto>>> GetAsync()
    {
        var query = new AllCryptoTimeSeriesQuery();
        List<CryptoTimeSeries> timeSeries = await _mediator.Send(query);
        return Ok(DtoProvider.BuildCryptoCurrencyDto(timeSeries));
    }

    [HttpGet]
    [Route("{currencyName}")]
    public async Task<ActionResult<CryptoCurrencyDto>> GetAsync(string currencyName)
    {
        var query = new CryptoTimeSeriesQueryByName(currencyName);
        CryptoTimeSeries timeSeries = await _mediator.Send(query);
        return Ok(DtoProvider.BuildCryptoCurrencyDto(timeSeries));
    }
}
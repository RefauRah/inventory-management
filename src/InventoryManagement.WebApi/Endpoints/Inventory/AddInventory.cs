﻿using InventoryManagement.Core.Abstractions;
using InventoryManagement.Domain.Entities;
using InventoryManagement.Domain.Enums;
using InventoryManagement.Shared.Abstractions.Databases;
using InventoryManagement.Shared.Abstractions.Encryption;
using InventoryManagement.WebApi.Endpoints.Inventory.Requests;
using InventoryManagement.WebApi.Validators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Swashbuckle.AspNetCore.Annotations;

namespace InventoryManagement.WebApi.Endpoints.Inventory;

public class AddInventory : BaseEndpointWithoutResponse<AddInventoryRequest>
{
    private readonly IDbContext _dbContext;
    private readonly IInventoryService _inventoryService;
    private readonly IRng _rng;
    private readonly ISalter _salter;
    private readonly IStringLocalizer<AddInventory> _localizer;

    public AddInventory(IDbContext dbContext,
        IInventoryService InventoryService,
        IRng rng,
        ISalter salter,
        IStringLocalizer<AddInventory> localizer)
    {
        _dbContext = dbContext;
        _inventoryService = InventoryService;
        _rng = rng;
        _salter = salter;
        _localizer = localizer;
    }

    [HttpPost("add")]
    //[Authorize]
    //[RequiredScope(typeof(InventoryScope))]
    [SwaggerOperation(
        Summary = "Add Inventory",
        Description = "",
        OperationId = "Inventory.AddInventory",
        Tags = new[] { "Inventory" })
    ]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
    public override async Task<ActionResult> HandleAsync(AddInventoryRequest request,
        CancellationToken cancellationToken = new())
    {      
        try
        {
            var validator = new AddInventoryRequestValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
                return BadRequest(Error.Create(_localizer["invalid-parameter"], validationResult.Construct()));

            if (request.Qty < 0)
                return BadRequest(Error.Create(_localizer["invalid-parameter"]));

            var existingInventory = await _inventoryService.GetByBookIdAsync(request.BookId, cancellationToken);
            if (existingInventory != null)
            {
                _dbContext.AttachEntity(existingInventory);

                existingInventory.BookId = request.BookId;
                existingInventory.Stock += request.Qty;

                var transactionHistory = new TransactionHistory
                {
                    InventoryId = existingInventory.Id,
                    Qty = request.Qty,
                    TransactionDate = DateTime.UtcNow,
                    TransactionType = (int)TransactionType.In
                };

                await _dbContext.InsertAsync(transactionHistory, cancellationToken);
            }
            else
            {
                var newInventory = new Domain.Entities.Inventory
                {
                    BookId = request.BookId,
                    Stock = request.Qty,
                };

                await _dbContext.InsertAsync(newInventory, cancellationToken);

                var transactionHistory = new TransactionHistory
                {
                    InventoryId = newInventory.Id,
                    Qty = request.Qty,
                    TransactionDate = DateTime.UtcNow,
                    TransactionType = (int)TransactionType.In
                };

                await _dbContext.InsertAsync(transactionHistory, cancellationToken);
            }

            await _dbContext.SaveChangesAsync(cancellationToken);

            return NoContent();
        }
        catch (Exception ex)
        {
            // Log the exception or handle it accordingly
            throw new Exception(ex.Message, ex);
        }
    }
}
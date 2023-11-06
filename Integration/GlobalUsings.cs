global using Microsoft.AspNetCore.Builder;
global using Microsoft.AspNetCore.Hosting;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;

global using ApplicationCore.Entities;
global using ApplicationCore.DTOs.Product;
global using ApplicationCore.DTOs.Category;
global using ApplicationCore.Utilities.Constants;
global using ApplicationCore.Utilities.Results;
global using ApplicationCore.Interfaces.Repositories;
global using ApplicationCore.Interfaces.Services;

global using Infrastructure.Data;
global using Infrastructure.ExtensionMethods;

global using Integration.Base;
global using Integration.Fixtures;
global using Integration.Harness;

global using Xunit;
global using FluentAssertions;

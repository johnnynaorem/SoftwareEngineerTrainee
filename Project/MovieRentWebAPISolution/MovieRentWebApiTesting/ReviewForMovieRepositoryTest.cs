using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Moq;
using MovieRentWebAPI.Context;
using MovieRentWebAPI.Exceptions;
using MovieRentWebAPI.Models;
using MovieRentWebAPI.Repositories;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieRentWebApiTesting
{
    public class ReviewForMovieRepositoryTest
    {
        DbContextOptions<MovieRentContext> options;
        MovieRentContext context;
        ReviewRepository repository;

        [SetUp]
        public void Setup()
        {
            options = new DbContextOptionsBuilder<MovieRentContext>()
                .UseInMemoryDatabase("ReviewMemory")
                .Options;

            context = new MovieRentContext(options);
            repository = new ReviewRepository(context);
        }

        [Test]
        public async Task Add_Success_Test()
        {
            var customer = new Customer
            {
                CustomerId = 2,
                FullName = "Foo",
                Address =  "Address",
                PhoneNumber = "1234567890",
            };

            var movie = new Movie
            {
                MovieId = 1,
            };

            var review = new ReviewForMovie
            {
                Comment = "Comments on Movie",
                Rating = 2,
                MovieId = 1,
                CustomerId = 2,
            };

            var reviewEntity = new ReviewForMovie
            {
                Id = 1,
                Comment = "Comments on Movie",
                Rating = 2,
                MovieId = 1,
                CustomerId = 2,
            };

            var result = await repository.Add(review);
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.MovieId);
        }

        [Test]
        public async Task Add_CouldNotAddException_Test()
        {
            

            var review = new ReviewForMovie
            {
                Comment = "Comments on Movie",
                Rating = 2,
                MovieId = 1,
                CustomerId = 2,
            };

            var reviewEntity = new ReviewForMovie
            {
                Id = 1,
                Comment = "Comments on Movie",
                Rating = 2,
                MovieId = 1,
                CustomerId = 2,
            };

            context.Dispose();
            var result = Assert.ThrowsAsync<CouldNotAddException>
                (async () => await repository.Add(review));
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Message.Contains("Review Add Failed"));
        }

        [Test]
        public async Task GetReview_Success_Test()
        {
            var review = new ReviewForMovie
            {
                Comment = "Comments on Movie",
                Rating = 2,
                MovieId = 1,
                CustomerId = 2,
            };

            var reviewEntity = new ReviewForMovie
            {
                Id = 1,
                Comment = "Comments on Movie",
                Rating = 2,
                MovieId = 1,
                CustomerId = 2,
            };
            await repository.Add(review);
            var result = await repository.Get(1);
            
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Id == 1);
        }

        [Test]
        public async Task GetAllSuccess_Test()
        {
            var reviews = new List<ReviewForMovie>
            {
                new ReviewForMovie
                {
                    Id= 4,
                    Comment = "Comments on Movie",
                    Rating = 8,
                    MovieId = 11,
                    CustomerId = 22,
                },
                new ReviewForMovie
                {
                    Id= 6,
                    Comment = "Comments on Movie2",
                    Rating = 5,
                    MovieId = 21,
                    CustomerId = 31,
                }
            };
            await context.Reviews.AddRangeAsync(reviews);
            await context.SaveChangesAsync();
            var result = await repository.GetAll();

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Any());
        }

        [Test]
        public async Task GetAll_EmptyCollectionException_Test()
        {
            var _options = new DbContextOptionsBuilder<MovieRentContext>()
                .UseInMemoryDatabase("ReviewMemoryTest")
                .Options;

            var _context = new MovieRentContext(_options);
            var _repository = new ReviewRepository(_context);

            var result = Assert.ThrowsAsync<EmptyCollectionException>
                (async () => await _repository.GetAll());

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Message.Contains("Reviews Collection Empty"));
        }

        [Test]
        public async Task SaveChangesAsync_Test()
        {
            await context.SaveChangesAsync();
            Assert.Pass();
        }

        [Test]
        public async Task UpdateReview_Success_Test()
        {
            var review = new ReviewForMovie
            {
                Id = 1,
                Comment = "Initial Comment",
                Rating = 3,
                MovieId = 1,
                CustomerId = 2
            };

            await repository.Add(review);
            await context.SaveChangesAsync();

            var updatedReview = new ReviewForMovie
            {
                Id = 1,
                Comment = "Updated Comment",
                Rating = 5
            };

            var result = await repository.Update(updatedReview, 1);

            Assert.IsNotNull(result);
            Assert.AreEqual("Updated Comment", result.Comment);
            Assert.AreEqual(5, result.Rating);
            Assert.AreEqual(1, result.Id);
        }

        [Test]
        public async Task UpdateReview_NotFound_Test()
        {
            var updatedReview = new ReviewForMovie
            {
                Id = 999,
                Comment = "This review should not exist",
                Rating = 3
            };

            var ex = Assert.ThrowsAsync<InvalidOperationException>(async () =>
            {
                await repository.Update(updatedReview, 999);
            });

            Assert.IsNotNull(ex);
            Assert.AreEqual("Review not found with Id: 999", ex.Message);
        }

    }
}

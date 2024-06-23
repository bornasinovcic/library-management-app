/// <reference types="Cypress" />

import { localhost_url } from "../fixtures/example.json";

describe('Testing of book create, update and delete function', () => {

    let _bookTitle = '';
    let _bookDescription = '';
    let _bookPublishDate = '';
    let _bookGenre = '';
    let _bookAuthors = [];

    beforeEach(() => {
        cy.visit(localhost_url);
    });

    it('Create new book', () => {
        cy.get('a.nav-link.text-dark[href="/Book"]').click();
        cy.get('a.btn.btn-success[href="/Book/Create"]').click();

        let randomNumber = Math.floor(10000 + Math.random() * 90000);
        let bookTitle = `Title${randomNumber}`;
        let bookDescription = `Description${randomNumber}`;
        let bookPublishDate = getRandomDate();

        cy.get('input#Title')
            .clear()
            .type(bookTitle)
            .should('have.value', bookTitle);
        cy.get('input#Description')
            .clear()
            .type(bookDescription)
            .should('have.value', bookDescription);
        cy.get('#PublishDate')
            .clear()
            .type(bookPublishDate)
            .should('have.value', bookPublishDate);

        cy.get('#GenreId').then($select => {
            let optionsCount = $select.find('option').length;
            let randomIndex = getRandomInt(1, optionsCount - 1); // Skip the first option (index 0)

            cy.get('#GenreId').select($select.find('option').eq(randomIndex).val()).then(() => {
                _bookGenre = $select.find('option').eq(randomIndex).text(); // Save the selected genre text
                cy.log('Selected genre:', _bookGenre);
            });
        });

        cy.get('#AuthorBooks').then($select => {
            let optionsCount = $select.find('option').length + 1;
            let numberOfSelections = getRandomInt(1, optionsCount); // Random number of selections
            let randomIndexes = getRandomIndexes(numberOfSelections, optionsCount); // Get random indexes

            let selectedValues = randomIndexes.map(index => $select.find('option').eq(index).val());
            cy.get('#AuthorBooks').select(selectedValues).then(() => {
                _bookAuthors = randomIndexes.map(index => $select.find('option').eq(index).text()); // Save the selected authors text
                cy.log('Selected authors:', _bookAuthors.join(', '));
            });
        });

        cy.then(() => {
            cy.log(`The bookTitle saved is: ${bookTitle}`);
            cy.log(`The bookDescription saved is: ${bookDescription}`);
            cy.log(`The bookPublishDate saved is: ${bookPublishDate}`);
            cy.log(`The bookGenre saved is: ${_bookGenre}`);
            cy.log(`The bookAuthors saved is: ${_bookAuthors.join(', ')}`);
        });

        cy.then(() => {
            _bookTitle = bookTitle;
            _bookDescription = bookDescription;
            _bookPublishDate = bookPublishDate;

            cy.log('Creating book with the following details:');
            cy.log(`Title: ${_bookTitle}`);
            cy.log(`Description: ${_bookDescription}`);
            cy.log(`Publish Date: ${_bookPublishDate}`);
            cy.log(`Genre: ${_bookGenre}`);
            cy.log(`Authors: ${_bookAuthors.join(', ')}`);
        });

        cy.get('input[type="submit"][value="Create"].btn.btn-outline-success.mt-2').click();
    });

    it('Update already existing book', () => {
        cy.get('a.nav-link.text-dark[href="/Book"]').click();

        cy.get('input#Title')
            .clear()
            .type(_bookTitle)
            .should('have.value', _bookTitle);

        cy.get('button.btn.btn-outline-info').click();

        cy.contains('tr', _bookTitle).within(() => {
            cy.get('a.btn.btn-primary').click();
        });

        let randomNumber = Math.floor(10000 + Math.random() * 90000);
        let bookTitle = `Title${randomNumber}`;
        let bookDescription = `Description${randomNumber}`;
        let bookPublishDate = getRandomDate();

        cy.get('input#Title')
            .clear()
            .type(bookTitle)
            .should('have.value', bookTitle);
        cy.get('input#Description')
            .clear()
            .type(bookDescription)
            .should('have.value', bookDescription);
        cy.get('#PublishDate')
            .clear()
            .type(bookPublishDate)
            .should('have.value', bookPublishDate);

        cy.get('#GenreId').then($select => {
            let optionsCount = $select.find('option').length;
            let randomIndex = getRandomInt(1, optionsCount - 1); // Skip the first option (index 0)

            cy.get('#GenreId').select($select.find('option').eq(randomIndex).val()).then(() => {
                _bookGenre = $select.find('option').eq(randomIndex).text(); // Save the selected genre text
                cy.log('Selected genre:', _bookGenre);
            });
        });

        cy.get('#AuthorBooks').then($select => {
            let optionsCount = $select.find('option').length + 1;
            let numberOfSelections = getRandomInt(1, optionsCount); // Random number of selections
            let randomIndexes = getRandomIndexes(numberOfSelections, optionsCount); // Get random indexes

            let selectedValues = randomIndexes.map(index => $select.find('option').eq(index).val());
            cy.get('#AuthorBooks').select(selectedValues).then(() => {
                _bookAuthors = randomIndexes.map(index => $select.find('option').eq(index).text()); // Save the selected authors text
                cy.log('Selected authors:', _bookAuthors.join(', '));
            });
        });

        cy.then(() => {
            cy.log(`The bookTitle saved is: ${bookTitle}`);
            cy.log(`The bookDescription saved is: ${bookDescription}`);
            cy.log(`The bookPublishDate saved is: ${bookPublishDate}`);
            cy.log(`The bookGenre saved is: ${_bookGenre}`);
            cy.log(`The bookAuthors saved is: ${_bookAuthors.join(', ')}`);
        });

        cy.then(() => {
            _bookTitle = bookTitle;
            _bookDescription = bookDescription;
            _bookPublishDate = bookPublishDate;

            cy.log('Creating book with the following details:');
            cy.log(`Title: ${_bookTitle}`);
            cy.log(`Description: ${_bookDescription}`);
            cy.log(`Publish Date: ${_bookPublishDate}`);
            cy.log(`Genre: ${_bookGenre}`);
            cy.log(`Authors: ${_bookAuthors.join(', ')}`);
        });

        cy.get('input[type="submit"][value="Save edit"].btn.btn-outline-success.mt-2').click();

    });

    it('Delete already existing book', () => {

        cy.get('a.nav-link.text-dark[href="/Book"]').click();

        cy.get('input#Title')
            .clear()
            .type(_bookTitle)
            .should('have.value', _bookTitle);

        cy.get('button.btn.btn-outline-info').click();

        cy.contains('tr', _bookTitle).within(() => {
            cy.get('button.btn.btn-danger').click();
        });

    });

});

function getRandomDate() {
    let start = new Date(1900, 1, 1); // Start date - change to -90 years
    let end = new Date(); // End date (current date)
    let randomDate = new Date(start.getTime() + Math.random() * (end.getTime() - start.getTime()));

    // Format the date as M/D/YYYY without leading zeros
    let day = randomDate.getDate();
    let month = randomDate.getMonth() + 1; // Months are 0-based in JavaScript
    let year = randomDate.getFullYear();

    return `${month}/${day}/${year}`;
}

function getRandomInt(min, max) {
    return Math.floor(Math.random() * (max - min) + min);
}

function getRandomIndexes(count, max) {
    let indexes = new Set();
    while (indexes.size < count) {
        indexes.add(getRandomInt(0, max - 1));
    }
    return Array.from(indexes);
}
/// <reference types="Cypress" />

import { localhost_url } from "../fixtures/example.json";
import { getRandomCapitalLetter } from "../support/utils.js";

describe('Verify warnings for required parameters', () => {

    beforeEach(() => {
        cy.visit(localhost_url);
    });

    it('Verify warning for required parameters in author creation', () => {
        let randomLetter = getRandomCapitalLetter();

        cy.get('a.nav-link.text-dark[href="/Author"]').click();
        cy.get('a.btn.btn-success[href="/Writer/Add"]').click();
        cy.get('input[type="submit"][value="Create"].btn.btn-outline-success.mt-2').click();

        cy.get('span.text-danger.field-validation-error[data-valmsg-for="FirstName"][data-valmsg-replace="true"]')
            .should('be.visible')
            .and('contain', 'First name is required.');
        cy.get('span.text-danger.field-validation-error[data-valmsg-for="LastName"][data-valmsg-replace="true"]')
            .should('be.visible')
            .and('contain', 'Last name is required.');
        cy.get('span.text-danger.field-validation-error[data-valmsg-for="Gender"][data-valmsg-replace="true"]')
            .should('be.visible')
            .and('contain', 'The value \'\' is invalid.');
        cy.get('span.text-danger.field-validation-error[data-valmsg-for="Email"][data-valmsg-replace="true"]')
            .should('be.visible')
            .and('contain', 'Email address is required.');
        cy.get('span.text-danger.field-validation-error[data-valmsg-for="DateOfBirth"][data-valmsg-replace="true"]')
            .should('be.visible')
            .and('contain', 'The value \'\' is invalid.');

        cy.get('input#FirstName').clear().type(randomLetter);
        cy.get('input#LastName').clear().type(randomLetter);
        cy.get('input#Gender').clear().type(randomLetter);

        cy.get('input[type="submit"][value="Create"].btn.btn-outline-success.mt-2').click();

        cy.get('span.text-danger.field-validation-error[data-valmsg-for="Gender"][data-valmsg-replace="true"]')
            .should('be.visible')
            .and('contain', "Gender must be 'M' or 'F'.");
        cy.get('span.text-danger.field-validation-error[data-valmsg-for="FirstName"][data-valmsg-replace="true"]')
            .should('be.visible')
            .and('contain', 'First name must be between 2 and 50 characters.');
        cy.get('span.text-danger.field-validation-error[data-valmsg-for="LastName"][data-valmsg-replace="true"]')
            .should('be.visible')
            .and('contain', 'Last name must be between 2 and 50 characters.');
    });

    it('Verify warning for required parameters in book creation', () => {
        let randomLetter = getRandomCapitalLetter();

        cy.get('a.nav-link.text-dark[href="/Book"]').click();
        cy.get('a.btn.btn-success[href="/Book/Create"]').click();
        cy.get('input[type="submit"][value="Create"].btn.btn-outline-success.mt-2').click();

        cy.get('span.text-danger.field-validation-error[data-valmsg-for="Title"][data-valmsg-replace="true"]')
            .should('be.visible')
            .and('contain', 'Title is required.');
        cy.get('span.text-danger.field-validation-error[data-valmsg-for="Description"][data-valmsg-replace="true"]')
            .should('be.visible')
            .and('contain', 'Description is required.');
        cy.get('span.text-danger.field-validation-error[data-valmsg-for="PublishDate"][data-valmsg-replace="true"]')
            .should('be.visible')
            .and('contain', 'The value \'\' is invalid');

        cy.get('input#Title').clear().type(randomLetter);

        cy.get('input[type="submit"][value="Create"].btn.btn-outline-success.mt-2').click();

        cy.get('span.text-danger.field-validation-error[data-valmsg-for="Title"][data-valmsg-replace="true"]')
            .should('be.visible')
            .and('contain', 'Title must be between 2 and 100 characters.');
    });

    it('Verify warning for required parameters in city creation', () => {
        let randomLetter = getRandomCapitalLetter();

        cy.get('a.nav-link.text-dark[href="/City"]').click();
        cy.get('a.btn.btn-success[href="/Place/Add"]').click();
        cy.get('input[type="submit"][value="Create"].btn.btn-outline-success.mt-2').click();

        cy.get('span.text-danger.field-validation-error[data-valmsg-for="Name"][data-valmsg-replace="true"]')
            .should('be.visible')
            .and('contain', 'City name is required.');
        cy.get('span.text-danger.field-validation-error[data-valmsg-for="Country"][data-valmsg-replace="true"]')
            .should('be.visible')
            .and('contain', 'Country name is required.');

        cy.get('input#Name').clear().type(randomLetter);
        cy.get('input#Country').clear().type(randomLetter);

        cy.get('input[type="submit"][value="Create"].btn.btn-outline-success.mt-2').click();

        cy.get('span.text-danger.field-validation-error[data-valmsg-for="Name"][data-valmsg-replace="true"]')
            .should('be.visible')
            .and('contain', 'City name must be between 2 and 50 characters.');
        cy.get('span.text-danger.field-validation-error[data-valmsg-for="Country"][data-valmsg-replace="true"]')
            .should('be.visible')
            .and('contain', 'Country name must be between 2 and 50 characters.');
    });

    it('Verify warning for required parameters in genre ceation', () => {
        let randomLetter = getRandomCapitalLetter();

        cy.get('a.nav-link.text-dark[href="/Genre"]').click();
        cy.get('a.btn.btn-success[href="/Genre/Create"]').click();
        cy.get('input[type="submit"][value="Create"].btn.btn-outline-success.mt-2').click();

        cy.get('span.text-danger.field-validation-error[data-valmsg-for="Name"][data-valmsg-replace="true"]')
            .should('be.visible')
            .and('contain', 'Genre name is required.');

        cy.get('input#Name').clear().type(randomLetter);

        cy.get('input[type="submit"][value="Create"].btn.btn-outline-success.mt-2').click();

        cy.get('span.text-danger.field-validation-error[data-valmsg-for="Name"][data-valmsg-replace="true"]')
            .should('be.visible')
            .and('contain', 'Genre name must be between 2 and 50 characters.');
    });

})
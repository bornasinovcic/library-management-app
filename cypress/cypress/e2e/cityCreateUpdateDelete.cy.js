/// <reference types="Cypress" />

import { localhost_url } from "../fixtures/example.json";

describe('Testing of city create, update and delete function', () => {

    let _cityName = '';
    let _countryName = '';

    beforeEach(() => {
        cy.visit(localhost_url);
    });

    it('Create new city', () => {
        cy.get('a.nav-link.text-dark[href="/City"]').click();
        cy.get('a.btn.btn-success[href="/Place/Add"]').click();

        let randomNumber = Math.floor(10000 + Math.random() * 90000);
        let cityName = `City${randomNumber}`;
        let countryName = `Country${randomNumber}`;

        _cityName = cityName;
        _countryName = countryName;

        cy.log(`The cityName saved is: ${cityName}`);
        cy.log(`The countryName saved is: ${countryName}`);

        cy.get('input#Name')
            .clear()
            .type(cityName)
            .should('have.value', cityName);
        cy.get('input#Country')
            .clear()
            .type(countryName)
            .should('have.value', countryName);

        cy.get('input[type="submit"][value="Create"].btn.btn-outline-success.mt-2').click();
    });

    it('Update already existing city', () => {
        cy.get('a.nav-link.text-dark[href="/City"]').click();

        cy.get('input#Name')
            .clear()
            .type(_cityName)
            .should('have.value', _cityName);
        cy.get('input#Country')
            .clear()
            .type(_countryName)
            .should('have.value', _countryName);

        cy.get('button.btn.btn-outline-info').click();

        cy.contains('tr', _cityName).within(() => {
            cy.get('a.btn.btn-primary').click();
        });

        let randomNumber = Math.floor(10000 + Math.random() * 90000);
        let cityName = `City${randomNumber}`;
        let countryName = `Country${randomNumber}`;

        _cityName = cityName;
        _countryName = countryName;

        cy.log(`The cityName saved is: ${cityName}`);
        cy.log(`The countryName saved is: ${countryName}`);

        cy.get('input#Name')
            .clear()
            .type(cityName)
            .should('have.value', cityName);
        cy.get('input#Country')
            .clear()
            .type(countryName)
            .should('have.value', countryName);

        cy.get('input[type="submit"][value="Save edit"].btn.btn-outline-success.mt-2').click();

    });

    it('Delete already existing city', () => {
        cy.get('a.nav-link.text-dark[href="/City"]').click();

        cy.get('input#Name')
            .clear()
            .type(_cityName)
            .should('have.value', _cityName);
        cy.get('input#Country')
            .clear()
            .type(_countryName)
            .should('have.value', _countryName);

        cy.get('button.btn.btn-outline-info').click();

        cy.contains('tr', _cityName).within(() => {
            cy.get('button.btn.btn-danger').click();
        });

    });

})
/// <reference types="Cypress" />

import { localhost_url } from "../fixtures/example.json";

describe("", () => {

    beforeEach(() => {
        cy.visit(localhost_url);
    });

    it("Deletion of used city will be stopped", () => {
        cy.get("a.nav-link.text-dark[href='/Author']").click();
        cy.get('table.table.table-condensed tbody tr:first-child td:nth-child(4)')
            .invoke('text')
            .then((city) => {
                cy.wrap(city).as('firstRowCity');
            });
        cy.get("a.nav-link.text-dark[href='/City']").click();
        cy.get('@firstRowCity').then((city) => {
            cy.get("input#Name")
                .clear()
                .type(city)
                .should("have.value", city);
            cy.get("button.btn.btn-outline-info").click();
            cy.contains("tr", city).within(() => {
                cy.get("button.btn.btn-danger").click();
            });
            cy.on('window:alert', (alertText) => {
                expect(alertText).to.equal('That city is being used so it cannot be deleted.');
            });
            cy.get("tr").contains(city).click();
            cy.get("button.btn.btn-danger").click();
            cy.get('.alert.alert-danger')
                .should('be.visible')
                .and('contain.text', 'Unable to delete city. It is being used somewhere in the application.');
        });
    });

    it("Deletion of used genre will be stopped", () => {
        cy.get("a.nav-link.text-dark[href='/Book']").click();
        cy.get('table.table.table-condensed tbody tr:first-child td:nth-child(4)')
            .invoke('text')
            .then((genre) => {
                cy.wrap(genre).as('firstRowGenre');
            });
        cy.get("a.nav-link.text-dark[href='/Genre']").click();
        cy.get('@firstRowGenre').then((genre) => {
            cy.get("input#Name")
                .clear()
                .type(genre)
                .should("have.value", genre);
            cy.get("button.btn.btn-outline-info").click();
            cy.contains("tr", genre).within(() => {
                cy.get("button.btn.btn-danger").click();
            });
            cy.on('window:alert', (alertText) => {
                expect(alertText).to.equal('That genre is being used so it cannot be deleted.');
            });
            cy.get("tr").contains(genre).click();
        });
        cy.get("button.btn.btn-danger").click();
        cy.get('.alert.alert-danger')
            .should('be.visible')
            .and('contain.text', 'Unable to delete genre. It is being used somewhere in the application.');
    });

})
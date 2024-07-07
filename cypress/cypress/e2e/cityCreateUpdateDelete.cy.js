/// <reference types="Cypress" />

import { localhost_url } from "../fixtures/example.json";

let _randomNumber = "";
let _cityName = "";
let _countryName = "";

function fillForm() {
    _randomNumber = Math.floor(10000 + Math.random() * 90000);
    _cityName = `City${_randomNumber}`;
    _countryName = `Country${_randomNumber}`;
    cy.get("input#Name")
        .clear()
        .type(_cityName)
        .should("have.value", _cityName);
    cy.get("input#Country")
        .clear()
        .type(_countryName)
        .should("have.value", _countryName);
}

function searchAndVerify(testCondition = "defaultCondition") {
    cy.get("input#Name")
        .clear()
        .type(_cityName)
        .should("have.value", _cityName);
    cy.get("input#Country")
        .clear()
        .type(_countryName)
        .should("have.value", _countryName);
    cy.get("button.btn.btn-outline-info").click();
    if (testCondition === "create" || testCondition === "update") {
        cy.get("tr").contains(_cityName).click();
        cy.get("dd#name").should("contain", _cityName);
        cy.get("dd#country").should("contain", _countryName);
    } else if (testCondition === "delete") {
        cy.get(".table.table-condensed tbody").find("tr").should("have.length", 0);
    }
}

describe("Testing of city create, update and delete function", () => {

    beforeEach(() => {
        cy.visit(localhost_url);
    });

    it("Create new city", () => {
        cy.get("a.nav-link.text-dark[href='/City']").click();
        cy.get("a.btn.btn-success[href='/Place/Add']").click();
        fillForm();
        cy.get("input[type='submit'][value='Create'].btn.btn-outline-success.mt-2").click();
        searchAndVerify("create");
    });

    it("Update already existing city", () => {
        cy.get("a.nav-link.text-dark[href='/City']").click();
        searchAndVerify();
        cy.contains("tr", _cityName).within(() => {
            cy.get("a.btn.btn-primary").click();
        });
        fillForm();
        cy.get("input[type='submit'][value='Save edit'].btn.btn-outline-success.mt-2").click();
        searchAndVerify("update");
    });

    it("Delete already existing city", () => {
        cy.get("a.nav-link.text-dark[href='/City']").click();
        searchAndVerify()
        cy.contains("tr", _cityName).within(() => {
            cy.get("button.btn.btn-danger").click();
        });
        searchAndVerify("delete");
    });

})
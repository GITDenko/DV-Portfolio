import React from "react";
import { Nav, Navbar, Container } from "react-bootstrap";

const Header = () => {

    return(
        <Navbar bg="light" expand="lg">
            <Container>
                <Navbar.Brand href="/Home">DV PORTFOLIO</Navbar.Brand>
                <Navbar.Toggle aria-controls="basic-navbar-nav" />
                <Navbar.Collapse id="basic-navbar-nav">
                    <Nav>
                        <Nav.Link href="/Home">Home</Nav.Link>
                        <Nav.Link href="/Admin">Admin</Nav.Link>
                    </Nav>
                </Navbar.Collapse>
            </Container>
        </Navbar>
    );
}

export default Header;
import React from "react";
import { Nav, Navbar, Container, NavDropdown } from "react-bootstrap";

const Header = () => {

    return(
        <Navbar bg="light" expand="lg">
            <Container>
                <Navbar.Brand href="/Home">DV PORTFOLIO</Navbar.Brand>
                <Navbar.Toggle aria-controls="basic-navbar-nav" />
                <Navbar.Collapse id="basic-navbar-nav">
                    <Nav>
                        <Nav.Link href="/Home">Home</Nav.Link>
                        <NavDropdown title="Admin" id="basic-nav-dropdown">
                            <NavDropdown.Item href="/adminmaincategory">Main Categories</NavDropdown.Item>
                            <NavDropdown.Item href="/adminsubcategory">Subcategories</NavDropdown.Item>
                            <NavDropdown.Divider />
                            <NavDropdown.Item href="/adminphotos">Photos</NavDropdown.Item>
                            <NavDropdown.Item href="/adminvideos">Videos</NavDropdown.Item>
                            <NavDropdown.Item href="/adminwebsites">Websites</NavDropdown.Item>
                        </NavDropdown>
                    </Nav>
                </Navbar.Collapse>
            </Container>
        </Navbar>
    );
}

export default Header;
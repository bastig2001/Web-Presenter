import React from 'react';
import PresentationMenu from "./PresentationMenu";
import Presentation from "./Presentation";
import TextService from "../services/TextService";

export default class AudienceView extends React.Component {
    constructor(props) {
        super(props);
        this.state = {text: {__html: "wait for it"}};
        
        TextService.connection.on('textChanged', text => {
            this.setState({text: text});
        })
    }
    
    render() {
        return (
            <section className="AudienceViw">
                <PresentationMenu/>
                <Presentation text={this.state.text}/>
            </section>
        );
    }
}

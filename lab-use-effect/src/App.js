import logo from './logo.svg';				
import './App.css';
import React, { useState, useEffect } from "react";

window.addEventListener("resize", Title);

/* export default function Title(props) {
	const [count, setTitle] = useState(0);
	useEffect(() => {
		document.title = sizeName;
		});
};
function Settitle() {
	let sizeName;

	let reportWindowSize = window.innerWidth;
	if (reportWindowSize < 767) {
	sizeName = "small";
	}
	else if (reportWindowSize < 1199) {
	sizeName = "medium";
	}
	else {
	sizeName = "large";
	}
} */

export default function Title(props) {
	const [title, setTitle] = useState("small");
	useEffect(() => {
	let reportWindowSize = window.innerWidth;
	
	if (reportWindowSize < 767) {
	var newTitle = "small";
	}
	else if (reportWindowSize < 1199) {
	var newTitle = "medium";
	}
	else {
	var newTitle = "large";
	}
	setTitle(newTitle);
	}, []);
	// Specify how to clean up after this effect:
	return (
	<div>
	<div> Timer {props.name} </div>
	<div> Title is {title} </div>
	</div>
	);
}

function App() {
  return (
    <div className="App">
      <header className="App-header">



      </header>
    </div>
  );
}

//export default App;

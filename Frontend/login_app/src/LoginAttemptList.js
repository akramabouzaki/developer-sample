import React from "react";
import "./LoginAttemptList.css";

const LoginAttemptList = (props) => (
	<div className="Attempt-List-Main">
	 	<p>Recent activity</p>
	  	<input type="input" placeholder="Filter..." />
		<ul className="Attempt-List">
		{props.attempts.map((attempt, index) =><li key={index}>{attempt}</li>)}
		</ul>
	</div>
);

export default LoginAttemptList;

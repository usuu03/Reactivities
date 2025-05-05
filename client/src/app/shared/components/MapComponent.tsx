import { MapContainer, Marker, Popup, TileLayer } from "react-leaflet";
import "leaflet/dist/leaflet.css";

type Props = {
	position: [number, number];
	venue: string;
};

export default function MapComponent(props: Props) {
	return (
		<MapContainer
			center={props.position}
			zoom={13}
			scrollWheelZoom={false}
			style={{ height: "100%" }}
		>
			<TileLayer url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png" />
			<Marker position={props.position}>
				<Popup>{props.venue}</Popup>
			</Marker>
		</MapContainer>
	);
}

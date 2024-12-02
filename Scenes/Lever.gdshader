shader_type canvas_item;

uniform vec4 color:source_color;
uniform float speed:hint_range(0.0, 10.0, 1.);
void vertex() {
	if(VERTEX.x > 0.0)
	{
		VERTEX += vec2(sin(TIME)*speed, cos(TIME)*speed);
	}
}

void fragment() {
	COLOR = mix(texture(TEXTURE, UV), color, (sin(TIME)+1.0)*0.5);
}

//void light() {
	// Called for every pixel for every light affecting the CanvasItem.
	// Uncomment to replace the default light processing function with this one.
//}

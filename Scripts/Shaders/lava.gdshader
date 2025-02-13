shader_type canvas_item;
uniform float speed = -0.2;
uniform vec4 color:source_color;

void vertex() {
	// Called for every vertex the material is visible on.
}

void fragment() {
	vec4 _texture =  texture(TEXTURE, vec2(UV.x + TIME * speed, UV.y));
	vec4 _final_texture = _texture;
	_final_texture.rgb = mix(color.rgb, _final_texture.rgb,0);
	COLOR = _final_texture;

}

//void light() {
	// Called for every pixel for every light affecting the CanvasItem.
	// Uncomment to replace the default light processing function with this one.
//}

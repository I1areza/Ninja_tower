shader_type canvas_item;
uniform float clip_x:hint_range(0.0, 1.0, 0.01) = 1.;

void fragment() {
	if(UV.x < clip_x)
	{
		COLOR = texture(TEXTURE, UV);
	}else
	{
		COLOR = vec4(0.);
	}
}

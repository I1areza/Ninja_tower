shader_type canvas_item;
group_uniforms OutlineControls;
//uniform float max_outline_alpha:hint_range(0.0,1., 0.05);
uniform int outline_width:hint_range(0, 5, 1);
uniform vec4 outline_color:source_color;
group_uniforms;
group_uniforms FlickeringControls;
uniform bool enable_flickering;
uniform float flickering_speed:hint_range(0.0, 50.0, 1.);
uniform float minimal_flickering_alpha_ratio:hint_range(0.0, 0.9, 0.1);
group_uniforms;
group_uniforms TextureControls;
uniform float texture_alpha:hint_range(0.0, 1.0, 0.01);
group_uniforms;

void fragment() {
	float pixel_size_x = 1.0/float(textureSize(TEXTURE,0).x);
	float pixel_size_y = 1.0/float(textureSize(TEXTURE,0).y);
	vec2 pixel_size = vec2(pixel_size_x, pixel_size_y);
	if (texture(TEXTURE, UV).a < 0.001)
	{ 
		for(int x = -outline_width; x <= outline_width; x++)
		{
			for(int y = -outline_width; y <= outline_width; y++)
			{
					vec2 pixel_offset = vec2(float(x),float(y));
					if( texture(TEXTURE, UV+(pixel_offset*pixel_size)).a > 0.)
					{
						vec4 resulting_color = outline_color;
						float alpha_norm = (sin(TIME*flickering_speed) +1.0) * 0.5*outline_color.a;
						if(enable_flickering == true)
						{
					 
							alpha_norm = ((1.- minimal_flickering_alpha_ratio) * alpha_norm + minimal_flickering_alpha_ratio*outline_color.a);
						}
						else
						{
							alpha_norm = outline_color.a;
						}
						
						
						
					
						resulting_color.a = alpha_norm;
					COLOR = resulting_color;
					}
			}
		}
	}else
	{
		
				vec4 resultingColor = texture(TEXTURE, UV);
				resultingColor.a = texture_alpha;
				COLOR = resultingColor;
			
		
		
	}
}






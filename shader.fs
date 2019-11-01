#version 330 core
struct Material {
	sampler2D diffuse;
	sampler2D specular;
	float shininess;
};

struct Light {
	vec3 position;
	vec3 direction;
	float cutOff;

	vec3 ambient;
	vec3 diffuse;
	vec3 specular;

	float constant;
	float linear;
	float quadratic;
};

out vec4 FragColor;  

uniform Light light;
uniform Material material;
uniform vec3 viewPos;

in vec3 Normal;
in vec3 FragPos;
in vec2 TexCoords;


void main()
{
	vec3 lightDir = normalize(light.position - FragPos);
	float theta = dot(lightDir, normalize(-light.direction));

	if(theta > light.cutOff)
	{
		// attenuation
		float distance = length(light.position - FragPos);
		float attenuation = 1.0 / (light.constant + light.linear * distance + light.quadratic * (distance * distance));

		// ambient
		vec3 ambient = light.ambient * texture(material.diffuse, TexCoords).rgb;

		//diffuse
		vec3 norm = normalize(Normal);
		
		float diff = max(dot(norm,lightDir), 0.0);
		vec3 diffuse = light.diffuse * diff * texture(material.diffuse,TexCoords).rgb;
	
		// specular
		vec3 viewDir = normalize(viewPos - FragPos);
		vec3 reflectDir = reflect(-lightDir, norm);
		//calculate specular component
		float spec = pow(max(dot(viewDir, reflectDir), 0.0), material.shininess);
		vec3 specular =  light.specular * spec * texture(material.specular, TexCoords).rgb;

		//ambient *= attenuation;
		diffuse *= attenuation;
		specular *= attenuation;

		FragColor = vec4(ambient + diffuse + specular, 1.0);
	}
	else // use ambient light 
	{
		FragColor = vec4(light.ambient * vec3(texture(material.diffuse, TexCoords)), 1.0);
	}
}
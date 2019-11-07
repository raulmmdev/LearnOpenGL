#pragma once

#include "glm/glm.hpp"
#include <vector>
#include <string>
#include "Shader.h"

using namespace std;

struct Vertex {
	glm::vec3 Position;
	glm::vec3 Normal;
	glm::vec2 TexCoords;
};

struct Texture {
	unsigned int id;
	string type;
	string path;
};

class Mesh
{
public:
	//mesh data
	vector<Vertex> vertices;
	vector<unsigned int> indices;
	
	//functions
	Mesh(vector<Vertex> vertices, vector<unsigned int> indices, vector<Texture> textures);
	void Draw(Shader shader);
private:
	vector<Texture> textures;
	//render data
	unsigned int VAO, VBO, EBO;
	
	void setupMes();
};


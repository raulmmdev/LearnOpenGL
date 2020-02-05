#pragma once

#include "Shader.h"
#include "Mesh.h"
#include <vector>
#include <iostream>
#include <assimp/Importer.hpp>
#include <assimp/scene.h>

using namespace std;

class Model
{
public:
	vector<Mesh> meshes;
	vector<Texture> textures_loaded;
	Model(const string& path);

	void Draw(Shader shader);
private:
	//model data
	
	string directory;
	

	void loadModel(const string& path);
	void processNode(aiNode* node, const aiScene* scene);
	Mesh processMesh(aiMesh* mesh, const aiScene* scene);
	vector<Texture> loadMaterialTextures(aiMaterial* mat, aiTextureType type, string typeName);
};


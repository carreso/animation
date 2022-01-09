using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloth : MonoBehaviour
{
    public int heightSpring;
    public int widthSpring;
    public int nbOfParticlesperHeight;
    public int nbOfParticlesperWidth;

    public GameObject ballPrefab;
    public GameObject springPrefab;
    public List<Particle> particlesList = new List<Particle>();
    public List<Spring> springsList = new List<Spring>();

    public float mass;
    public float elasticity_spring;
    public float dampening_spring;
    public float elasticity_part;

    public int return1DIndexfrom2D(int i, int j, int width)
    {
        return i * width + j;
    }


    // Start is called before the first frame update
    void Start()
    {
        Debug.Assert((nbOfParticlesperHeight % 2 == 1) && (nbOfParticlesperWidth % 2 == 1) && nbOfParticlesperHeight >= 1 && nbOfParticlesperWidth >= 1);

        //Creating the cloth as a rectangle of size width x height
        createMeshCloth();
        
        //Creating the springs between all the particles
        for (int i = 0; i < nbOfParticlesperWidth - 1; i++)
        {
            for (int j = 0; j < nbOfParticlesperHeight - 1; j++)
            {

                int particleIndex = i * this.nbOfParticlesperWidth + j;

                //Spring connecting the particle with the one its right & add it to its list of neighbours
                createSpringBetweenPart1Part2(particleIndex, particleIndex + 1);

                //Spring connecting the particle with the one below it & add it to its list of neighbours
                createSpringBetweenPart1Part2(particleIndex, particleIndex + this.nbOfParticlesperWidth);


                //Spring connecting the particle with the one on its right below diagonal
                createSpringBetweenPart1Part2(particleIndex, particleIndex + this.nbOfParticlesperWidth + 1);



            }
        }

        //Then we need to add some springs not covered by the precedent loop 
        //Last row
        for (int j = 0; j < nbOfParticlesperHeight - 1; j++)
        {
            int particleIndexRow = (nbOfParticlesperHeight - 1) * nbOfParticlesperWidth + j; //last row part
            createSpringBetweenPart1Part2(particleIndexRow, particleIndexRow + 1); //its right neighbour
        }

        //Last column
        for (int i = 0; i < nbOfParticlesperWidth - 1; i++)
        {
            int particleIndexColumn = (i + 1) * (nbOfParticlesperWidth) - 1; //last column part
            createSpringBetweenPart1Part2(particleIndexColumn, particleIndexColumn + nbOfParticlesperWidth); //its below neighbour

        }

        // //Bending Springs
        for (int i = 0; i < nbOfParticlesperWidth -2; i++)
        {
            for (int j = 0; j < nbOfParticlesperHeight - 2; j++)
            {
                int particleIndex = return1DIndexfrom2D(i,j, nbOfParticlesperWidth);
                createSpringBetweenPart1Part2(particleIndex, return1DIndexfrom2D(i, j+2, nbOfParticlesperWidth));


                
                createSpringBetweenPart1Part2(particleIndex, return1DIndexfrom2D(i +2, j, nbOfParticlesperWidth));
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    /*Instantiating a spring between two particles knowing the index of those part in the list*/
    public void createSpringBetweenPart1Part2(int indexPart1, int indexPart2)
    {
        GameObject spr = Instantiate(springPrefab, this.transform);
        Spring spring = spr.GetComponent<Spring>();
        spring.InitializeSpring(this.elasticity_spring, this.dampening_spring, particlesList[indexPart1], particlesList[indexPart2]);
        springsList.Add(spring);

    }



    public void createMeshCloth()
    {
        for (int i = 0; i < nbOfParticlesperWidth; i++)
        {
            for (int j = 0; j < nbOfParticlesperHeight; j++)
            {
                // they will all be on the plane y
                GameObject part = Instantiate(ballPrefab, this.transform);
                //Debug.Log("position" + this.transform.position);
                Particle particle = part.GetComponent<Particle>();
                Vector3 value = new Vector3(j * widthSpring / (float)(nbOfParticlesperWidth - 1), 0.0f, -i * heightSpring / (float)(nbOfParticlesperHeight - 1));
                Vector3 initialPosition = this.transform.position + value;
                //Debug.Log("initpos" + initialPosition);
                //Debug.Log("value" + value);
                particle.InitializeParticle(initialPosition, Vector3.zero, elasticity_part, 0.07f, mass);
                particle.Move();

                //We fix the particles of the first line
                if (i == 0)
                {
                    particle.ball1 = true;
                }

                //Add it in the list of all particles
                particlesList.Add(particle);


            }
        }
        Debug.Log(particlesList.ToString());


    }

    public void simulateSpringForces()
    {

        foreach (Spring spring in springsList)
        {
            
            spring.ApplySpringForces();

        }
    }
}
